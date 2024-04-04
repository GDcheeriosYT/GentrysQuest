using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Entity
{
    public partial class HitBox : CompositeDrawable
    {
        private const bool DEBUG = false;
        private bool enabled = true;
        public readonly AffiliationType Affilation;

        private Quad collisionQuad
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public HitBox(AffiliationType affilation)
        {
            Affilation = affilation;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Colour = Colour4.Red;
            Alpha = (float)(DEBUG ? .5 : 0);
            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both
            };
        }

        public void Disable() { enabled = false; }

        public void Enable() { enabled = true; }

        public bool CheckCollision(DrawableEntity entity)
        {
            if (enabled) return collisionQuad.Intersects(entity.HitBox) && Affilation != entity.Affiliation;
            return false;
        }
    }
}
