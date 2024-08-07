﻿using System;
using System.Collections.Generic;
using System.Linq;
using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Content.Enemies;
using GentrysQuest.Game.Content.Maps;
using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Location.Drawables;
using GentrysQuest.Game.Online.API.Requests;
using GentrysQuest.Game.Overlays.Inventory;
using GentrysQuest.Game.Overlays.Notifications;
using GentrysQuest.Game.Screens.Gameplay.Results;
using GentrysQuest.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class Gameplay : Screen
    {
        public int Score { get; set; } = new();
        private int spendableScore;
        private TextFlowContainer scoreFlowContainer;
        private SpriteText scoreText;
        private DrawablePlayableEntity playerEntity;
        private readonly List<DrawableEntity> enemies = new();
        private readonly List<Projectile> projectiles = new();
        private GameplayHud gameplayHud;
        private DrawableMap map;
        private InventoryOverlay inventoryOverlay;

        private bool showingInventory = false;

        /// <summary>
        /// Maximum enemies allowed to spawn at once
        /// </summary>
        private int enemySpawnLimit;

        /// <summary>
        /// How many enemies are allowed on the screen
        /// </summary>
        private int enemyLimit;

        /// <summary>
        /// The gameplay difficulty
        /// </summary>
        private int gameplayDifficulty;

        /// <summary>
        /// The gameplay time tracker
        /// </summary>
        private double gameplayTime;

        /// <summary>
        /// if the gameplay is paused
        /// </summary>
        private bool isPaused;

        /// <summary>
        /// The amount of pause time
        /// </summary>
        private double pauseTime;

        private const double MAX_TIME_TO_SPAWN = 20000;

        private delegate void GameplayEvent();

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = ColourInfo.GradientVertical(Color4.DarkGray, Color4.White),
                    RelativeSizeAxes = Axes.Both
                },
                gameplayHud = new GameplayHud(),
                map = new DrawableMap(),
                inventoryOverlay = new InventoryOverlay(),
                scoreFlowContainer = new TextFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight
                },
            };
            scoreFlowContainer.AddText(scoreText = new SpriteText
            {
                Text = "0",
                Colour = Colour4.Black,
                Font = FontUsage.Default.With(size: 64)
            });
            scoreFlowContainer.AddText(new SpriteText
            {
                Text = "score",
                Colour = Colour4.Black,
                Font = FontUsage.Default.With(size: 52),
                Padding = new MarginPadding { Left = 15 }
            });
            SetUp();
        }

        /// <summary>
        /// Add an enemy to the gameplay scene
        /// </summary>
        /// <param name="level">Current level of the character</param>
        /// <param name="enemy">For if you want to use custom enemies</param>
        public void AddEnemy(int level, Enemy enemy = null)
        {
            enemy ??= new TestEnemy(1);
            enemy.Experience.Level.Current.Value = level;
            enemy.UpdateStats();
            DrawableEnemyEntity newEnemy = new DrawableEnemyEntity(enemy);
            newEnemy.Position = new Vector2(MathBase.RandomInt(-500, 500), MathBase.RandomInt(-500, 500));
            AddInternal(newEnemy);
            enemies.Add(newEnemy);
            enemy.SetWeapon();
            newEnemy.GetEntityObject().OnDeath += delegate { Scheduler.AddDelayed(() => RemoveEnemy(newEnemy), 100); };
            newEnemy.GetEntityObject().OnDeath += delegate
            {
                bool notValidArtifact = true;
                int spendAmount = (int)(Math.Pow(gameplayDifficulty + 1, 2) * 1000);

                if (spendableScore >= spendAmount)
                {
                    spendableScore -= spendAmount;

                    while (notValidArtifact)
                    {
                        string familyString = map.MapReference.Families[MathBase.RandomChoice(map.MapReference.Families.Count)].Name;
                        int starRating = MathBase.GetStarRating(gameplayDifficulty);
                        Artifact artifact = GameData.Content.GetFamily(familyString).GetArtifact();

                        if (artifact.ValidStarRatings.Contains(starRating))
                        {
                            artifact.Initialize(starRating);
                            GameData.Add(artifact);
                            notValidArtifact = false;
                        }
                    }
                }
            };
            newEnemy.FollowEntity(playerEntity);
            playerEntity.SetEntities(enemies);
        }

        /// <summary>
        /// Spawns enemies in bulk
        /// </summary>
        public void SpawnEntities()
        {
            int currentAmount = 0;

            while (enemies.Count < enemyLimit)
            {
                AddEnemy(HelpMe.GetScaledLevel(gameplayDifficulty, playerEntity.GetEntityObject().Experience.Level.Current.Value));
                currentAmount++;
                if (currentAmount > enemySpawnLimit) break;
            }
        }

        public void SetDifficulty(int difficulty) => gameplayDifficulty = difficulty;

        public void SetDifficulty()
        {
            gameplayDifficulty = map.MapReference.Difficulty;
            if (map.MapReference.DifficultyScales) gameplayDifficulty += playerEntity.GetEntityObject().Difficulty;
            enemyLimit = (gameplayDifficulty + 1) * 2;
        }

        public void Pause()
        {
            playerEntity.GetEntityObject().AddEffect(new Paused());

            foreach (DrawableEntity enemy in enemies)
            {
                enemy.GetEntityObject().AddEffect(new Paused());
            }

            isPaused = true;
        }

        public void UnPause()
        {
            playerEntity.GetEntityObject().RemoveEffect("Paused");

            foreach (DrawableEntity enemy in enemies)
            {
                enemy.GetEntityObject().RemoveEffect("Paused");
            }

            isPaused = false;
        }

        /// <summary>
        /// Get the right time to spawn enemies
        /// </summary>
        /// <returns>if it's right to spawn</returns>
        private void spawnEnemyClock()
        {
            if (!isPaused)
            {
                double elapsedTime = Clock.CurrentTime - (gameplayTime - pauseTime);
                pauseTime = 0;

                if (MathBase.RandomInt(1, 10000 / 1 + gameplayDifficulty) < 5)
                {
                    if (!atEnemyLimit()) AddEnemy(HelpMe.GetScaledLevel(gameplayDifficulty, playerEntity.GetEntityObject().Experience.Level.Current.Value));
                }

                if (elapsedTime > MAX_TIME_TO_SPAWN)
                {
                    gameplayTime = Clock.CurrentTime;
                    SpawnEntities();
                }
            }
            else
            {
                pauseTime = Clock.CurrentTime;
            }
        }

        private bool atEnemyLimit()
        {
            return enemies.Count == enemyLimit;
        }

        /// <summary>
        /// Remove an enemy from the gameplay scene
        /// </summary>
        /// <param name="enemy">The enemy to remove</param>
        public void RemoveEnemy(DrawableEnemyEntity enemy)
        {
            HitBoxScene.Remove(enemy.HitBox);
            HitBoxScene.Remove(enemy.ColliderBox);
            HitBoxScene.Remove(enemy.Weapon.HitBox);
            enemies.Remove(enemy);
            RemoveInternal(enemy, false);
            playerEntity.SetEntities(enemies);
        }

        private void removeAllEnemies()
        {
            List<DrawableEntity> newEnemyList = new List<DrawableEntity>();

            foreach (var drawableEntity in enemies)
            {
                newEnemyList.Add(drawableEntity);
            }

            foreach (var drawableEntity in newEnemyList)
            {
                var enemy = (DrawableEnemyEntity)drawableEntity;
                RemoveEnemy(enemy);
            }
        }

        /// <summary>
        /// Sets up the gameplay scene
        /// </summary>
        public void SetUp()
        {
            map.Load(new TestMap());

            if (playerEntity is null)
            {
                AddInternal(playerEntity = new DrawablePlayableEntity(GameData.EquipedCharacter));
                if (GameData.EquipedCharacter.Weapon != null) GameData.EquipedCharacter.SetWeapon(GameData.EquipedCharacter.Weapon);
                SetDifficulty();
                playerEntity.OnMove += delegate(Vector2 direction, double speed)
                {
                    manage_direction(direction, speed, map);
                    foreach (DrawableEntity enemyEntity in enemies) manage_direction(direction, speed, enemyEntity);
                    foreach (Projectile projectile in projectiles) manage_direction(direction, speed, projectile);
                };
                playerEntity.GetEntityObject().OnDeath += End;
                playerEntity.GetEntityObject().OnLevelUp += SetDifficulty;
                playerEntity.GetEntityObject().OnLevelUp += delegate
                {
                    if (playerEntity.GetEntityObject().Experience.CurrentLevel() % 5 == 0)
                    {
                        removeAllEnemies();
                        Character character = GameData.Content.Characters[MathBase.RandomChoice(GameData.Content.Characters.Count)];
                        character.Experience.Level.Current.Value = playerEntity.GetEntityObject().Experience.CurrentLevel();
                        character.Stats.Health.point += 5;
                        WeaponChoices weaponChoices = new WeaponChoices();

                        switch (character.Name)
                        {
                            case "Brayden Messerschmidt":
                                Weapon weapon = new BraydensOsuPen();
                                weapon.Experience.Level.Current.Value = character.Experience.CurrentLevel();
                                weaponChoices.AddChoice(weapon, 100);
                                break;

                            default:
                                weaponChoices.AddChoice(new Knife(), 100);
                                break;
                        }

                        AddEnemy(playerEntity.GetEntityObject().Experience.CurrentLevel(), character.CreateEnemy(weaponChoices));
                    }
                };
            }

            Scheduler.AddDelayed(() =>
            {
                NotificationContainer.Instance.MoveToY(0.1f, 100);
            }, 100);

            gameplayHud.SetEntity(GameData.EquipedCharacter);
            playerEntity.SetupClickContainer();
            gameplayTime = Clock.CurrentTime;
            GameData.EquipedCharacter.Spawn();
            GameData.StartStatTracker();
            GameData.CurrentStats.ScoreStatistic.OnScoreChange += change =>
            {
                spendableScore += change;
                this.TransformTo(nameof(Score), (int)GameData.CurrentStats.ScoreStatistic.Value, 1000, Easing.Out);
            };
        }

        /// <summary>
        /// Manages how entities move depending on the direction.
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <param name="speed">The speed</param>
        /// <param name="drawable">The drawable to invoke movement on</param>
        private void manage_direction(Vector2 direction, double speed, Drawable drawable)
        {
            var value = (float)(Clock.ElapsedFrameTime * speed);
            drawable.MoveTo(drawable.Position + -direction * value);
        }

        /// <summary>
        /// Manages how to end the gameplay scene
        /// </summary>
        public void End()
        {
            if (GameData.CurrentUser.Value != null)
            {
                var scoreTask = new SubmitScoreRequest(GameData.CurrentUser.Value.ID, (long)GameData.CurrentStats.ScoreStatistic.Value).PerformAsync();
            }

            Pause();

            playerEntity.RemoveClickContainer();
            NotificationContainer.Instance.MoveToY(0);
            Container deathContainer = new Container
            {
                Alpha = 0,
                Depth = -4,
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new SpriteText
                    {
                        Text = "You died",
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Colour4.Red,
                        Font = FontUsage.Default.With(size: 86)
                    }
                }
            };
            AddInternal(deathContainer);
            gameplayHud.Delay(3000).Then().FadeOut();
            Scheduler.AddDelayed(() => map.Unload(), 3000);
            deathContainer.FadeIn(3000);
            Scheduler.AddDelayed(delegate { this.Push(new ResultScreen()); }, 3000);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.C:
                    inventoryOverlay.ToggleDisplay();
                    if (isPaused) UnPause();
                    else Pause();
                    break;
            }

            return base.OnKeyDown(e);
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            this.FadeInFromZero(500, Easing.OutQuint);
        }

        protected override void Update()
        {
            base.Update();
            spawnEnemyClock();
            scoreText.Text = "" + Score;

            foreach (Projectile projectile in playerEntity.QueuedProjectiles.ToList())
            {
                projectile.Position = new Vector2(500, -500);
                projectile.ShootFrom(playerEntity);
                Scheduler.AddDelayed(() =>
                {
                    RemoveInternal(projectile, false);
                    projectiles.Remove(projectile);
                    HitBoxScene.Remove(projectile.HitBox);
                }, projectile.Lifetime);
                playerEntity.QueuedProjectiles.Remove(projectile);
                AddInternal(projectile);
                projectiles.Add(projectile);
            }

            foreach (DrawableEntity enemy in enemies)
            {
                foreach (Projectile projectile in enemy.QueuedProjectiles.ToList())
                {
                    projectile.ShootFrom(enemy);
                    Scheduler.AddDelayed(() =>
                    {
                        RemoveInternal(projectile, false);
                        projectiles.Remove(projectile);
                        HitBoxScene.Remove(projectile.HitBox);
                    }, projectile.Lifetime);
                    enemy.QueuedProjectiles.Remove(projectile);
                    AddInternal(projectile);
                    projectiles.Add(projectile);
                }
            }
        }
    }
}
