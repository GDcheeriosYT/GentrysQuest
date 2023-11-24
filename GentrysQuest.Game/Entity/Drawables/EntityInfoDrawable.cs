using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

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
            Colour = Color4.Gray;
            CornerRadius = 0.2f;
            Margin = new MarginPadding(2);
            Size = new Vector2(0.8f, 100);
            InternalChildren = new Drawable[]
            {
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

