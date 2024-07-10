using osu.Framework;
using osu.Framework.Platform;
using System.Threading.Tasks;
using Velopack;

namespace GentrysQuest.Desktop
{
    public static class Program
    {
        public static async Task Main()
        {
            VelopackApp.Build().Run();
            using (GameHost host = Host.GetSuitableDesktopHost(@"Gentry's Quest"))
            using (osu.Framework.Game game = new GentrysQuestDesktop(true))
                host.Run(game);
        }
    }
}
