using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Entity
{
    public partial class HitBox : CompositeDrawable
    {
# if DEBUG
        private const bool DEBUG = true;
# else
        private const bool DEBUG = false;
# endif
        public bool Enabled = true;
        public readonly AffiliationType Affiliation;
        private dynamic parent;

        private Quad collisionQuad
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public HitBox(dynamic parent)
        {
            this.parent = parent;
            Affiliation = parent.Affiliation;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Colour = Colour4.Red;
            Alpha = (float)(DEBUG ? .2 : 0);
            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both
            };
            HitBoxScene.Add(this);
        }

        public void Disable() { Enabled = false; }

        public void Enable() { Enabled = true; }

        public dynamic GetParent() => parent;

        public bool CheckCollision(HitBox hitBox)
        {
            if (Enabled && !hitBox.Equals(this)) return collisionQuad.Intersects(hitBox.collisionQuad) && Affiliation != hitBox.Affiliation;

            return false;
        }
    }
}
