using System;
using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntityInfoDrawableList : GentrysQuestTestScene
    {
        private CompositeDrawable containerBox;
        private BasicScrollContainer scrollContainer;
        private int yVal;

        public TestSceneEntityInfoDrawableList()
        {
            yVal = 0;
            Add(containerBox = new Container
            {
                CornerRadius = 20,
                CornerExponent = 3,
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(0.8f),
                Masking = true,
                // MaskingSmoothness = 3,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.White,
                    },
                    scrollContainer = new BasicScrollContainer(Direction.Vertical)
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        ScrollbarAnchor = Anchor.TopLeft,
                        Padding = new MarginPadding(20f),
                        ScrollContent =
                        {
                        }
                    },
                }
            });
        }

        [Test]
        public void add_entity()
        {
            AddStep("Add entity", () =>
            {
                scrollContainer.ScrollContent.Add(new EntityInfoDrawable(new TestCharacter(Random.Shared.Next(1, 6))) { Y = yVal });
                yVal += 110;
            });
        }
    }
}
