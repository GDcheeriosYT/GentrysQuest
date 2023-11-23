using GentrysQuest.Game.Screens.Intro;
using NUnit.Framework;
using osu.Framework.Screens;

namespace GentrysQuest.Game.Tests.Visual.Screens
{
    [TestFixture]
    public partial class TestSceneIntro : GentrysQuestTestScene
    {
        private Intro intro;
        private ScreenStack screens;

        [Test]
        public virtual void PlayIntro()
        {
            AddStep("Restart", () =>
            {
                screens?.Expire();
                Add(screens = new ScreenStack());
                screens.Push(intro = new Intro());
            });
        }
    }
}
