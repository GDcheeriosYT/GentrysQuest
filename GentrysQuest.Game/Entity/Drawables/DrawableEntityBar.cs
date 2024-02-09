using GentrysQuest.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables;

public partial class DrawableEntityBar : CompositeDrawable
{
    // objects
    private ProgressBar healthProgressBar;
    private SpriteText entityName;

    public DrawableEntityBar(Entity entity)
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.BottomCentre;
        RelativeSizeAxes = Axes.Both;
        RelativePositionAxes = Axes.Both;
        Y -= 0.1f;
        Size = new Vector2(1f, 0.1f);
        InternalChildren = new Drawable[]
        {
            entityName = new SpriteText
            {
                Text = entity.name,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.BottomCentre
            },
            healthProgressBar = new ProgressBar(0, entity.stats.Health.Total())
        };
        healthProgressBar.ForegroundColour = Colour4.Red;
    }
}
