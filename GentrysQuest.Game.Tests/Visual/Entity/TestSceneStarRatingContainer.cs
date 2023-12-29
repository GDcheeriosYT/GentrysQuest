using System;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneStarRatingContainer : GentrysQuestTestScene
    {
        private StarRatingContainer starRatingContainer;

        public TestSceneStarRatingContainer()
        {
            Add(starRatingContainer = new StarRatingContainer(1)
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(1f)
            });
        }

        [Test]
        public void RandomizeValue()
        {
            AddStep("Randomize", () =>
            {
                starRatingContainer.starRating.Value = Random.Shared.Next(1, 5);
            });
        }

        [Test]
        public void SetValue()
        {
            AddSliderStep("Set the value", 1, 5, 1, i =>
            {
                starRatingContainer.starRating.Value = i;
            });
        }
    }
}
