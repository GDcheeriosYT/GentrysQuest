using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneStarRatingContainer : GentrysQuestTestScene
    {
        private StarRatingContainer starRatingContainer;

        public TestSceneStarRatingContainer()
        {
            Add(starRatingContainer = new StarRatingContainer());
        }
    }
}
