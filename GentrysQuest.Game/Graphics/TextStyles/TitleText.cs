using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace GentrysQuest.Game.Graphics.TextStyles
{
    public partial class TitleText : SpriteText
    {
        public TitleText(string text)
        {
            Text = text;
            Colour = Color4.Black;
            Anchor = Anchor.Centre;
            Origin = Anchor.BottomCentre;
            Y = -200;
            Font = FontUsage.Default.With(size: 120);
        }
    }
}
