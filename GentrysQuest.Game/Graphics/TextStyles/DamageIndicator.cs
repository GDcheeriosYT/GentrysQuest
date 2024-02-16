using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Graphics.TextStyles;

public partial class DamageIndicator : SpriteText
{
    public DamageIndicator(int damage)
    {
        Text = "" + damage;
        Colour = Colour4.Red;
        this.FadeOut(200).Then().;
        this.MoveToX(this.X + 20, duration: 200);
        this.MoveToY(this.Y - 20, duration: 200);
    }
}
