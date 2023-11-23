using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class StarRatingDrawable : SpriteIcon
    {
        private Bindable<bool> isEnabled = new Bindable<bool>(true);

        public StarRatingDrawable()
        {
            Icon = FontAwesome.Solid.Star;
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Colour = ColourInfo.GradientVertical(Colour4.DarkGray, Colour4.LightGray);
            Size = new Vector2(1f);
        }
    }
}
