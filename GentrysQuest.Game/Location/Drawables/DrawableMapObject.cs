using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Location.Drawables;

public partial class DrawableMapObject : CompositeDrawable
{
    public IMapObject MapObjectReference { get; }
    public AffiliationType Affiliation { get; }
    public CollisonHitBox Collider { get; }

    public DrawableMapObject(IMapObject mapObject)
    {
        MapObjectReference = mapObject;
        Affiliation = AffiliationType.None;
        RelativePositionAxes = Axes.Both;
        RelativeSizeAxes = Axes.Both;
        Size = mapObject.Size;
        Position = mapObject.Position;
        Colour = mapObject.Colour;
        if (mapObject.HasCollider) Collider = new CollisonHitBox(this);

        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativePositionAxes = Axes.Both,
                RelativeSizeAxes = Axes.Both
            },
            Collider
        };
    }
}
