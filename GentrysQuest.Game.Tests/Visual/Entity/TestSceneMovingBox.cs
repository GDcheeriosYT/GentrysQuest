using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneMovingBox : GentrysQuestTestScene
    {
        private DrawableEntity testEntity;
        private DrawableEntity testBrayden;

        public TestSceneMovingBox()
        {
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Green});
            Add(testEntity = new DrawableEntity(new GentrysQuest.Game.Entity.Entity()));
            testEntity.Y = 100;
            Add(testBrayden = new DrawableEntity(new BraydenMesserschmidt()));
        }
    }
}
