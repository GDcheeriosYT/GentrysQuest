using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntityInfoDrawable : GentrysQuestTestScene
    {
        private EntityInfoDrawable entityInfoDrawable;
        private CompositeDrawable containerBox;

        public TestSceneEntityInfoDrawable()
        {
            Add(containerBox = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(0.8f),
                Children = new Drawable[]
                {
                    new Circle
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = ColourInfo.GradientVertical(Colour4.LightGray, Colour4.White)
                    },
                    entityInfoDrawable = new EntityInfoDrawable()
                }
            });
        }
    }
}
