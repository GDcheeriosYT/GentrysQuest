using System;
using System.Collections.Generic;
using System.Linq;
using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntityInfoDrawableList : GentrysQuestTestScene
    {
        private EntityInfoListContainer container;

        public TestSceneEntityInfoDrawableList()
        {
            Add(container = new());
            int amount = 1;
            int starRating = 1;
            bool isRandomRating = false;

            AddStep("Clear", () => container.ClearList());
            AddStep("Add Entities", () =>
            {
                List<GentrysQuest.Game.Entity.Entity> entityList = Enumerable.Repeat(new TestCharacter(isRandomRating ? RandomStarRating() : starRating), amount)
                                                                             .Select(x => (GentrysQuest.Game.Entity.Entity)x)
                                                                             .ToList();
                container.AddFromList(entityList);
            });
            AddSliderStep("Star rating value", 1, 5, 1, i => starRating = i);
            AddSliderStep("Entity amount", 1, 50, 1, i => amount = i);
            AddToggleStep("Random star rating", b => isRandomRating = b);
        }

        private int RandomStarRating() { return Random.Shared.Next(1, 6); }
    }
}
