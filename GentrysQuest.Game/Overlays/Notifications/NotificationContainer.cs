using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Overlays.Notifications;

public partial class NotificationContainer : CompositeDrawable
{
    private const bool DEBUG = true;

    public NotificationContainer()
    {
        RelativePositionAxes = Axes.Both;
        RelativeSizeAxes = Axes.Both;
        Origin = Anchor.TopRight;
        Anchor = Anchor.TopRight;
        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.Black,
                Alpha = (float)(DEBUG ? 0.2f : 0)
            }
        };
    }
}
