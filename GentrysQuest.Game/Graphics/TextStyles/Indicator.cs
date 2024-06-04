using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Graphics.TextStyles;

public partial class Indicator : SpriteText
{
    public Indicator(int damage)
    {
        Text = $"{damage}";
        Anchor = Anchor.TopCentre;
        Origin = Anchor.BottomCentre;
        AllowMultiline = false;
    }

    /// <summary>
    /// Fades and moves the indicator text.
    /// </summary>
    /// <returns>The time it took to finish the transition</returns>
    public int FadeOut()
    {
        const int time = 500;
        this.MoveToX(X + 100, time, Easing.In);
        this.MoveToY(Y - 100, time, Easing.Out);
        this.Delay(time * 0.4f)
            .RotateTo(20, time)
            .ScaleTo(0.1f, time);
        this.FadeOut(time * 0.9);
        return time;
    }
}
