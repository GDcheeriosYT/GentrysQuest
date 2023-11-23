using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Overlays.UserMenu;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace GentrysQuest.Game.Screens.MainMenu
{
    public partial class MainMenu : Screen
    {
        private Box background;
        private TitleText title;
        private UserMenu userMenu;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                },
                title = new TitleText("Gentry's Quest")
                {
                    Alpha = 0
                },
                userMenu = new UserMenu()
            };
        }

        public void press_play()
        {
            title.FadeOut(200);
            userMenu.ToggleOn();
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            base.OnEntering(e);
            userMenu.ToggleOff();
            background.FadeColour(ColourInfo.GradientVertical(Color4.DarkGray, Color4.White), 120, Easing.In);
            title.Delay(120).Then().FadeIn(120);
        }
    }
}
