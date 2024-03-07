using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Screens.Gameplay;
using NUnit.Framework;
using osu.Framework.Screens;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Tests.Visual.Screens
{
    [TestFixture]
    public partial class TestSceneGameplay : GentrysQuestTestScene
    {
        private ScreenStack screens;
        private Gameplay gameplay;
        private Character theGuy;

        public TestSceneGameplay()
        {
            Add(screens = new ScreenStack());
            screens.Push(gameplay = new Gameplay());
        }

        [Test]
        public virtual void Start()
        {
            AddStep("start", () =>
            {
                theGuy = new TestCharacter(1);
                gameplay.SetUp(theGuy);
            });
            AddStep("End", () => gameplay.End());
        }

        [Test]
        public void Management()
        {
            AddStep("AddEnemy", () =>
            {
                gameplay.AddEnemy();
            });
        }
    }
}
