using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Overlays.Notifications;
using GentrysQuest.Game.Screens.LoadingScreen;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Screens;

namespace GentrysQuest.Game
{
    public partial class GentrysQuestGame(bool arcadeMode) : GentrysQuestGameBase
    {
        private ScreenStack screenStack;
        private readonly VersionText versionText = new VersionText("Super Dooper Beta");

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
            Add(versionText);
            Add(NotificationContainer.Instance);
            Add(new CursorContainer());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            screenStack.Push(new LoadingScreen());
        }
    }
}
