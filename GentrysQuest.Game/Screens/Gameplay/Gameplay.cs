using System.Collections.Generic;
using GentrysQuest.Game.Content.Enemies;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class Gameplay : Screen
    {
        private Bindable<int> score = new(0);
        private DrawablePlayableEntity playerEntity;
        private List<DrawableEntity> enemies = new List<DrawableEntity>();
        private GameplayHud gameplayHud;
        private Box testBox;

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
                testBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.1f),
                    Position = new Vector2(500),
                    Colour = Colour4.Black
                }
            };
        }

        /// <summary>
        /// Add an enemy to the gameplay scene
        /// </summary>
        public void AddEnemy(int level)
        {
            Enemy enemy = new TestEnemy(1);
            while (enemy.Experience.Level.current < level) enemy.LevelUp();
            DrawableEnemyEntity newEnemy = new DrawableEnemyEntity(enemy);
            newEnemy.Position = new Vector2(MathBase.RandomInt(-500, 500), MathBase.RandomInt(-500, 500));
            AddInternal(newEnemy);
            enemies.Add(newEnemy);
            enemy.SetWeapon();
            newEnemy.GetEntityObject().OnDeath += delegate { Scheduler.AddDelayed(() => RemoveEnemy(newEnemy), 100); };
            newEnemy.FollowEntity(playerEntity);
            playerEntity.SetEntities(enemies);
        }

        public void SpawnEntities()
        {
            for (int enemyCounter = 0; enemyCounter < enemySpawnLimit; enemyCounter++)
            {
                AddEnemy(playerEntity.GetEntityObject().Experience.Level.current);
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
                playerEntity.OnMove += delegate(MovementDirection direction, double speed)
                {
                    manage_direction(direction, speed, testBox);

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
        private void manage_direction(MovementDirection direction, double speed, Drawable drawable)
        {
            var value = (float)(Clock.ElapsedFrameTime * speed);

            switch (direction)
            {
                case MovementDirection.Down:
                    drawable.Y -= value;
                    break;

                case MovementDirection.Up:
                    drawable.Y += value;
                    break;

                case MovementDirection.Left:
                    drawable.X += value;
                    break;

                case MovementDirection.Right:
                    drawable.X -= value;
                    break;
            }
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
        }
    }
}
