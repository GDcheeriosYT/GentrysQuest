using GentrysQuest.Game.Online.API;
using osu.Framework;
using osu.Framework.Platform;

namespace GentrysQuest.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            _ = new APIAccess();
            using (GameHost host = Host.GetSuitableDesktopHost("Testy"))
            using (var game = new GentrysQuestTestBrowser())
                host.Run(game);
        }
    }
}
