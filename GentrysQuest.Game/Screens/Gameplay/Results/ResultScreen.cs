using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Graphics;
using GentrysQuest.Game.Online.API.Requests;
using GentrysQuest.Game.Overlays.Inventory;
using GentrysQuest.Game.Scoring;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK;

namespace GentrysQuest.Game.Screens.Gameplay.Results
{
    public partial class ResultScreen : Screen
    {
        private ResultsLeaderboard leaderboard;
        private LoadingIndicator loadingIndicator;
        private StatDrawableContainer statisticsContainer;
        private InventoryButton retryButton;
        private readonly int leaderboardId;

        public ResultScreen(int leaderboardId = 0)
        {
            this.leaderboardId = leaderboardId;
            AddInternal(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.White
            });
            AddInternal(leaderboard = new ResultsLeaderboard
            {
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.45f, 1),
            });
            AddInternal(statisticsContainer = new StatDrawableContainer
            {
                Size = new Vector2(0.45f, 1),
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            });

            AddInternal(retryButton = new InventoryButton("Retry")
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                Margin = new MarginPadding { Left = 10, Bottom = 10 }
            });

            retryButton.SetAction(delegate
            {
                GameData.WrapUpStats();
                GameData.Reset();
                this.Push(new MainMenu.MainMenu());
            });

            foreach (var statistic in GameData.CurrentStats.GetStats().Where(statistic => statistic.Name != "Score"))
            {
                statisticsContainer.AddStat(new StatDrawable(statistic.Name, statistic.Value, false));
            }
        }

        private static async Task<List<LeaderboardPlacement>> fetchLeaderboard(int id)
        {
            var leaderboardResult = new GetLeaderboardRequest(id);
            await leaderboardResult.PerformAsync();
            return leaderboardResult.Response;
        }

        protected override async void LoadComplete()
        {
            base.LoadComplete();

            if (leaderboardId > 0)
            {
                var placements = await fetchLeaderboard(leaderboardId);
                Populate(placements);
            }
        }

        public void Populate(List<LeaderboardPlacement> placements)
        {
            int tracker = 1;

            foreach (LeaderboardPlacement placement in placements)
            {
                Scheduler.AddDelayed(() =>
                {
                    leaderboard.AddListing(placement);
                }, tracker * 50);

                tracker++;
            }
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            this.FadeInFromZero(500, Easing.OutQuint);
        }

        public override void OnSuspending(ScreenTransitionEvent e)
        {
            this.FadeOutFromOne(500, Easing.OutQuint);
        }
    }
}
