using GentrysQuest.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Screens.MainMenu
{
    public partial class MainMenuButton : GQButton
    {
        public MainMenuButton(string text)
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray
                },
                new SpriteText
                {
                    Text = text,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 24),
                    Colour = Colour4.Black
                }
            };
        }
    }
}
