using System.Collections.Generic;
using GentrysQuest.Game.Content.Enemies;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class Gameplay : Screen
    {
        private Bindable<int> score = new(0);
        private DrawablePlayableEntity playerEntity;
        private List<DrawableEntity> enemies;

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
                }
            };
        }

        public void AddEnemy()
        {
            DrawableEnemyEntity newEnemy = new DrawableEnemyEntity(new TestEnemy(3));
            AddInternal(newEnemy);
            newEnemy.FollowEntity(playerEntity);
        }

        public void SetUp(Character character)
        {
            if (playerEntity is null)
            {
                AddInternal(playerEntity = new(character));
                playerEntity.SetupClickContainer();
                // AddInternal(clickContainer = new GameplayClickContainer(playerEntity));
            }
        }

        public void End()
        {
            playerEntity.RemoveClickContainer();
            RemoveInternal(playerEntity, true);
            playerEntity.Dispose();
            playerEntity = null;
            // RemoveInternal(clickContainer, true);
            // clickContainer.Dispose();
            // clickContainer = null;
        }
    }
}
