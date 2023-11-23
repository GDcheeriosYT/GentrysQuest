using osu.Framework;
using osu.Framework.Platform;

namespace GentrysQuest.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost("Testy"))
            using (var game = new GentrysQuestTestBrowser())
                host.Run(game);
        }
    }
}
