using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity
{
    public partial class CollisonHitBox : HitBox
    {
        public CollisonHitBox(DrawableEntity parent)
            : base(parent)
        {
            Colour = Colour4.LimeGreen;
        }
    }
}
