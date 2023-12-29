using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityIconDrawable : Sprite
    {
        public EntityIconDrawable()
        {
            RelativePositionAxes = Axes.Both;
            Scale = new Vector2(0.32f);
        }
    }
}
