using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntityInfoDrawable : GentrysQuestTestScene
    {
        private EntityInfoDrawable entityInfoDrawable;
        private Container containerBox;

        public TestSceneEntityInfoDrawable()
        {
            Add(containerBox = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(0.8f),
                Colour = Colour4.Aquamarine,
                Child = entityInfoDrawable = new EntityInfoDrawable()
            });
        }
    }
}
