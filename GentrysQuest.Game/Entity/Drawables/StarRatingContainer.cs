using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class StarRatingContainer : CompositeDrawable
    {
        private readonly Bindable<int> starRating = new Bindable<int>();
        private StarRatingDrawable starRatingDrawable1;
        private StarRatingDrawable starRatingDrawable2;
        private StarRatingDrawable starRatingDrawable3;
        private StarRatingDrawable starRatingDrawable4;
        private StarRatingDrawable starRatingDrawable5;

        public StarRatingContainer()
        {
            Origin = Anchor.CentreLeft;
            Size = new Vector2(264, 124);
            Anchor = Anchor.BottomCentre;

            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.5f, 0.2f),
                Content = new[]
                {
                    new[]
                    {
                        starRatingDrawable1 = new StarRatingDrawable(),
                        starRatingDrawable2 = new StarRatingDrawable(),
                        starRatingDrawable3 = new StarRatingDrawable(),
                        starRatingDrawable4 = new StarRatingDrawable(),
                        starRatingDrawable5 = new StarRatingDrawable()
                    }
                }
            };
        }
    }
}
