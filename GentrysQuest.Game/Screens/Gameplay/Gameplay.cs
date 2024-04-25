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

        private int enemySpawnLimit = 4;

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

            inventoryButton.SetAction(inventoryOverlay.ToggleDisplay);
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
        }

        /// <summary>
        /// Add an enemy to the gameplay scene
        /// </summary>
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
            newEnemy.GetEntityObject().OnDeath += delegate { GameData.Artifacts.Add(new OsuTablet()); };
            newEnemy.GetEntityObject().OnDamage += delegate { score.Value += HIT_SCORE; };
            newEnemy.GetEntityObject().OnCrit += delegate { score.Value += CRIT_SCORE; };
            newEnemy.FollowEntity(playerEntity);
            playerEntity.SetEntities(enemies);
        }

        public void SpawnEntities()
        {
            for (int enemyCounter = 0; enemyCounter < enemySpawnLimit; enemyCounter++)
            {
                AddEnemy(playerEntity.GetEntityObject().Experience.Level.Current.Value);
            }
        }

        /// <summary>
        /// Remove an enemy from the gameplay scene
        /// </summary>
        /// <param name="enemy">The enemy to remove</param>
        public void RemoveEnemy(DrawableEnemyEntity enemy)
        {
            enemies.Remove(enemy);
            RemoveInternal(enemy, true);
            playerEntity.SetEntities(enemies);
        }

        /// <summary>
        /// Sets up the gameplay scene
        /// </summary>
        /// <param name="character"></param>
        public void SetUp(Character character)
        {
            if (playerEntity is null)
            {
                AddInternal(playerEntity = new DrawablePlayableEntity(character));
                if (character.Weapon != null) character.SetWeapon(character.Weapon);
                playerEntity.SetupClickContainer();
                playerEntity.OnMove += delegate(float direction, double speed)
                {
                    manage_direction(direction, speed, map);

                    foreach (DrawableEntity enemyEntity in enemies)
                    {
                        manage_direction(direction, speed, enemyEntity);
                    }
                };
            }

            gameplayHud.SetEntity(character);
            character.Spawn();
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
            playerEntity.RemoveClickContainer();
            RemoveInternal(playerEntity, true);
            playerEntity.Dispose();
            playerEntity = null;
            GameData.WrapUpStats();
        }
    }
}
