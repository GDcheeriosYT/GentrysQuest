using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoDrawable : CompositeDrawable
    {
        private EntityIconDrawable icon;
        private SpriteText name;
        private StarRatingContainer starRatingContainer;

        // [Resolved]
        // private TextureStore textures { get; }

        public EntityInfoDrawable()
        {
            RelativeSizeAxes = Axes.X;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            CornerRadius = 0.2f;
            Margin = new MarginPadding(2);
            Size = new Vector2(0.8f, 100);
            InternalChildren = new Drawable[]
            {
                new Circle
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
                    BorderColour = Colour4.Black,
                    BorderThickness = 2f
                },
                icon = new EntityIconDrawable
                {
                    Origin = Anchor.CentreLeft,
                    RelativePositionAxes = Axes.Both,
                    Scale = new Vector2(64, 64),
                    Anchor = Anchor.CentreLeft,
                    X = 0.05f
                },
                new StarRatingContainer()
            };
        }
    }
}

