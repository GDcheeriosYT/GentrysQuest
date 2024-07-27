using GentrysQuest.Game.Online.API.Requests;
using GentrysQuest.Game.Overlays.LoginOverlay;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Tests.Visual.Overlays
{
    [TestFixture]
    public partial class LoginOverlayTestScene : GentrysQuestTestScene
    {
        private LoginOverlay loginOverlay;
        private LoginRequest loginRequest;

        public LoginOverlayTestScene()
        {
            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.White
            });
            Add(loginOverlay = new LoginOverlay());
        }

        [Test]
        public void TestRequest()
        {
            AddStep("Create request", () => { loginRequest = new LoginRequest("test", "1234"); });
            AddStep("Perform request", () => { _ = loginRequest.PerformAsync(); });
            AddStep("Log result", () => { Logger.Log(loginRequest.Response.ToString(), LoggingTarget.Information); });
        }
    }
}
