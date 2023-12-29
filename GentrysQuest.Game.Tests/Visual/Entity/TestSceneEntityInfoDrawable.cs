using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntityInfoDrawable : GentrysQuestTestScene
    {
        private EntityInfoDrawable entityInfoDrawable;
        private GentrysQuest.Game.Entity.Entity testEntity;

        public TestSceneEntityInfoDrawable()
        {
            Add(entityInfoDrawable = new EntityInfoDrawable(testEntity = new TestCharacter(1)));
            AddSliderStep("Change the star rating", 1, 5, 1, i =>
            {
                entityInfoDrawable.starRatingContainer.starRating.Value = i;
            });
        }
    }
}
