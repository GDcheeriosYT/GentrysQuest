using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Screens.Gameplay;
using GentrysQuest.Game.Screens.Intro;
using GentrysQuest.Game.Screens.MainMenu;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace GentrysQuest.Game
{
    public partial class GentrysQuestGame : GentrysQuestGameBase
    {
        private ScreenStack screenStack;
        private readonly VersionText versionText = new VersionText("Super Dooper Beta");
        private bool arcadeMode;

        public GentrysQuestGame(bool arcadeMode)
        {
            this.arcadeMode = arcadeMode;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
            Add(versionText);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (arcadeMode) screenStack.Push(new Intro(new Gameplay()));
            else screenStack.Push(new Intro(new MainMenu()));
        }
    }
}
