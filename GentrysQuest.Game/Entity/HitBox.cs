using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;

namespace GentrysQuest.Game.Entity
{
    public partial class HitBox : CompositeDrawable
    {
        private const bool DEBUG = true;
        public readonly Affiliation affilation;

        private Quad collisionQuad
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public HitBox(Affiliation affilation)
        {
            this.affilation = affilation;

        }
    }
}
