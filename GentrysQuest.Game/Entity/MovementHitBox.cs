using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity
{
    public partial class MovementHitBox : HitBox
    {
        public MovementHitBox(DrawableEnemyEntity parent)
            : base(parent)
        {
            Colour = Colour4.Blue;
        }
    }
}
