using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Graphics;

public partial class ProgressBar : CompositeDrawable
{
    // values
    private Bindable<double> min;
    private Bindable<double> cur; // current value
    private Bindable<double> max;

    private Colour4 foregroundColour = Colour4.Blue;
    private Colour4 backgroundColour = Colour4.Gray;

    // events
    public delegate void ProgressChangeEvent();

    public event ProgressChangeEvent OnProgressChange;

    // objects
    private Box foreground;
    private Box background;

    public ProgressBar(double min, double max)
    {
        this.min = new Bindable<double>(min);
        cur = new Bindable<double>(0f);
        this.max = new Bindable<double>(max);
        Size = new Vector2(1f);
        InternalChildren = new Drawable[]
        {
            background = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = backgroundColour
            },
            foreground = new Box
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1f),
                Colour = foregroundColour
            }
        };
    }

    private void checkCurrent()
    {
        if (cur.Value > max.Value) cur.Value = max.Value;
        else if (cur.Value < min.Value) cur.Value = min.Value;
    }

    public double Current
    {
        get => cur.Value;
        set
        {
            cur.Value = value;
            checkCurrent();
            setProgress();
        }
    }

    public double Min
    {
        get => min.Value;
        set
        {
            min.Value = value;
            setProgress();
        }
    }

    public double Max
    {
        get => max.Value;
        set
        {
            max.Value = value;
            setProgress();
        }
    }

    public Colour4 BackgroundColour
    {
        get => backgroundColour;
        set => background.Colour = value;
    }

    public Colour4 ForegroundColour
    {
        get => foregroundColour;
        set => foreground.Colour = value;
    }

    private void setProgress()
    {
        if (max.Value > 0) foreground.ResizeWidthTo((float)(cur.Value / max.Value), 250, easing: Easing.OutCirc);
        OnProgressChange?.Invoke();
    }
}
