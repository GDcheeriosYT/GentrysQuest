using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntity : GentrysQuestTestScene
    {
        private GentrysQuest.Game.Entity.Entity entity;
        private DrawableEntity drawableEntity;

        public TestSceneEntity()
        {
            entity = new TestCharacter(3);
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = ColourInfo.GradientVertical(Colour4.LightGray, Colour4.Azure) });
            Add(drawableEntity = new DrawableEntity(entity));
        }

        [Test]
        public virtual void Start()
        {
            AddStep("Damage", () => entity.Damage(100));
            AddStep("Heal", () => entity.Heal(100));
        }
    }
}
