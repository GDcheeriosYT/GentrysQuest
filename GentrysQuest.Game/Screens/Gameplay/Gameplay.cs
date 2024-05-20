using System.Collections.Generic;
using GentrysQuest.Game.Content.Enemies;
using GentrysQuest.Game.Content.Families.BraydenMesserschmidt;
using GentrysQuest.Game.Content.Maps;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Location.Drawables;
using GentrysQuest.Game.Overlays.Inventory;
using GentrysQuest.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class Gameplay : Screen
    {
        private Bindable<int> score = new();
        private TextFlowContainer scoreFlowContainer;
        private SpriteText scoreText;
        private DrawablePlayableEntity playerEntity;
        private List<DrawableEntity> enemies = new List<DrawableEntity>();
        private GameplayHud gameplayHud;
        private DrawableMap map;
        private InventoryOverlay inventoryOverlay;
        private InventoryButton inventoryButton;

        private bool showingInventory = false;

        // Scoring
        private const int HIT_SCORE = 10;
        private const int CRIT_SCORE = 20;
        private const int KILL_SCORE = 100;

        /// <summary>
        /// Maximum enemies allowed to spawn at once
        /// </summary>
        private int enemySpawnLimit = 4;

        /// <summary>
        /// How many enemies are allowed on the screen
        /// </summary>
        private int enemyLimit = 4;

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
                map = new DrawableMap(new TestMap()),
                inventoryOverlay = new InventoryOverlay(),
                inventoryButton = new InventoryButton("Inventory")
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.15f, 0.06f),
                    Position = new Vector2(0.005f),
                },
                scoreFlowContainer = new TextFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight
                },
            };

            inventoryButton.SetAction(delegate
            {
                inventoryOverlay.ToggleDisplay();
                isPaused = !isPaused;
            });
            scoreFlowContainer.AddText(scoreText = new SpriteText
            {
                Text = "0",
                Colour = Colour4.Black,
                Font = FontUsage.Default.With(size: 58)
            });
            scoreFlowContainer.AddText(new SpriteText
            {
                Text = "score",
                Colour = Colour4.Black,
                Font = FontUsage.Default.With(size: 48),
                Padding = new MarginPadding { Left = 15 }
            });
            score.ValueChanged += delegate { scoreText.Text = $"{score}"; };
            SetUp();
        }

        /// <summary>
        /// Add an enemy to the gameplay scene
        /// </summary>
        /// <param name="level">Current level of the character</param>
        public void AddEnemy(int level)
        {
            Enemy enemy = new TestEnemy(1);
            while (enemy.Experience.Level.Current.Value < level) enemy.LevelUp();
            DrawableEnemyEntity newEnemy = new DrawableEnemyEntity(enemy);
            newEnemy.Position = new Vector2(MathBase.RandomInt(-500, 500), MathBase.RandomInt(-500, 500));
            AddInternal(newEnemy);
            enemies.Add(newEnemy);
            enemy.SetWeapon();
            newEnemy.GetEntityObject().OnDeath += delegate { Scheduler.AddDelayed(() => RemoveEnemy(newEnemy), 100); };
            newEnemy.GetEntityObject().OnDeath += delegate { score.Value += KILL_SCORE; };
            newEnemy.GetEntityObject().OnDamage += delegate { score.Value += HIT_SCORE; };
            newEnemy.GetEntityObject().OnCrit += delegate { score.Value += CRIT_SCORE; };
            newEnemy.GetEntityObject().OnDeath += delegate { GameData.Artifacts.Add(new OsuTablet()); };
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

        public void SetDifficulty()
        {
            gameplayDifficulty = map.MapReference.Difficulty;
            if (map.MapReference.DifficultyScales) gameplayDifficulty += playerEntity.GetEntityObject().Difficulty - 1;
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
        /// <param name="character"></param>
        public void SetUp()
        {
            if (playerEntity is null)
            {
                AddInternal(playerEntity = new DrawablePlayableEntity(GameData.EquipedCharacter));
                if (GameData.EquipedCharacter.Weapon != null) GameData.EquipedCharacter.SetWeapon(GameData.EquipedCharacter.Weapon);
                SetDifficulty();
                playerEntity.OnMove += delegate(float direction, double speed)
                {
                    manage_direction(direction, speed, map);

                    foreach (DrawableEntity enemyEntity in enemies)
                    {
                        manage_direction(direction, speed, enemyEntity);
                    }
                };
                playerEntity.GetEntityObject().OnDeath += End;
            }

            gameplayHud.SetEntity(GameData.EquipedCharacter);
            playerEntity.SetupClickContainer();
            gameplayTime = Clock.CurrentTime;
            GameData.EquipedCharacter.Spawn();
            GameData.StartStatTracker();
        }

        /// <summary>
        /// Manages how entities move depending on the direction.
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <param name="speed">The speed</param>
        /// <param name="drawable">The drawable to invoke movement on</param>
        private void manage_direction(float direction, double speed, Drawable drawable)
        {
            var value = (float)(Clock.ElapsedFrameTime * speed);

            drawable.MoveTo(drawable.Position + ((MathBase.GetAngleToVector(direction, true) * DrawableEntity.SLOWING_FACTOR) * value));
        }

        /// <summary>
        /// Manages how to end the gameplay scene
        /// </summary>
        public void End()
        {
            Container statContainer = new Container
            {

            };
            Container deathContainer = new Container
            {
                Alpha = 0,
                Depth = -4,
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Black
                    },
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
            isPaused = true;
            removeAllEnemies();
            playerEntity.RemoveClickContainer();
            GameData.WrapUpStats();
            RemoveInternal(playerEntity, true);
            playerEntity.Dispose();
            inventoryOverlay.Hide();
            inventoryButton.Hide();
            gameplayHud.Delay(3000).Then().FadeOut();
            map.Delay(3000).Then().FadeOut();
            deathContainer.FadeIn(3000).Then()
                          .Delay(1000).FadeOut(2000);
            scoreFlowContainer.FadeOut().Then()
                              .ScaleTo(3, 6100).Then()
                              .FadeIn(250).Then()
                              .Delay(2000).Then()
                              .MoveToY(-400, 250, Easing.InOutCirc);
            scoreFlowContainer.Anchor = Anchor.Centre;
            scoreFlowContainer.Origin = Anchor.Centre;
        }

        protected override void Update()
        {
            spawnEnemyClock();
            base.Update();
        }
    }
}
