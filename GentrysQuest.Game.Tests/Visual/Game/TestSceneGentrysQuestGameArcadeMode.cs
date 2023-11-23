using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace GentrysQuest.Game.Tests.Visual.Game
{
    [TestFixture]
    public partial class TestSceneGentrysQuestGameArcadeMode : GentrysQuestTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private GentrysQuestGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new GentrysQuestGame(true);
            game.SetHost(host);

            AddGame(game);
        }
    }
}
