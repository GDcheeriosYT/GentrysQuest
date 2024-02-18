using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Graphics.TextStyles;

public partial class Indicator : SpriteText
{
    public Indicator(int damage)
    {
        Text = "" + damage;
        Anchor = Anchor.TopCentre;
        Origin = Anchor.BottomCentre;
        Font = FontUsage.Default.With(size: 50);
        Colour = Colour4.Red;
    }
}
