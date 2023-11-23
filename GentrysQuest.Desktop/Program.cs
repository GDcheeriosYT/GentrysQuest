using osu.Framework.Platform;
using osu.Framework;
using GentrysQuest.Game;

namespace GentrysQuest.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Gentry's Quest"))
            using (osu.Framework.Game game = new GentrysQuestGame(false))
                host.Run(game);
        }
    }
}
