using GentrysQuest.Game.Database;
using GentrysQuest.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;

namespace GentrysQuest.Game.Screens.LoadingScreen
{
    public partial class LoadingScreen : Screen
    {
        private readonly LoadingIndicator indicator;
        private readonly SpriteText status;
        private byte progress = 0;

        public LoadingScreen()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black
                },
                indicator = new LoadingIndicator
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                status = new SpriteText
                {
                    Text = "Doin your mom",
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 50 },
                    Font = FontUsage.Default.With(size: 72)
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            GameData.Reset();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Scheduler.AddDelayed(() =>
            {
                indicator.FadeOut(300, Easing.InOutCirc);
                status.FadeOut(300);
            }, 1000);
            Scheduler.AddDelayed(() =>
            {
                status.FadeIn(300);
                status.Text = "Get Ready!";
                status.Margin = new MarginPadding();
                status.Anchor = Anchor.Centre;
                status.Origin = Anchor.Centre;
            }, 1500);
            Scheduler.AddDelayed(() => status.FadeOut(250), 2700);
            // Scheduler.AddDelayed(() => { this.Push(new Intro.Intro()); }, 3000);
            Scheduler.AddDelayed(() => { this.Push(new MainMenu.MainMenu()); }, 3000);
        }
    }
}
