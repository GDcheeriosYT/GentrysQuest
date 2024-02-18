using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoDrawable : CompositeDrawable
    {
        private EntityIconDrawable icon;
        private SpriteText name;
        public StarRatingContainer starRatingContainer;
        public Entity entity;
        private Colour4 firstColor = Colour4.Gray;
        private Colour4 secondColor = Colour4.Gray;

        // [Resolved]
        // private TextureStore textures { get; }

        public EntityInfoDrawable(Entity entity)
        {
            this.entity = entity;

            RelativeSizeAxes = Axes.X;
            Origin = Anchor.TopCentre;
            Anchor = Anchor.TopCentre;
            CornerRadius = 0.2f;
            Margin = new MarginPadding(2);
            Size = new Vector2(0.8f, 100);
            CornerExponent = 2;
            CornerRadius = 15;
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 2f;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.GradientHorizontal(firstColor, secondColor),
                },
                name = new SpriteText
                {
                    Text = entity.Name,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.TopLeft,
                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.2f),
                    Scale = new Vector2(2.5f),
                    AllowMultiline = false,
                    Truncate = true,
                    X = 0.2f,
                    Y = -0.5f
                },
                icon = new EntityIconDrawable
                {
                    Origin = Anchor.CentreLeft,
                    Anchor = Anchor.CentreLeft,
                    X = 0.05f
                },
                starRatingContainer = new StarRatingContainer(this.entity.StarRating.Value)
                {
                    Size = new Vector2(0.27f, 1f),
                    Y = 0.7f,
                    X = 0.2f
                },
            };

            starRatingContainer.starRating.BindValueChanged(updateColorWithStarRating, true);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            icon.Texture = textures.Get(entity.TextureMapping.Get("Idle"));
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(new Vector2(1.2f, 1f), 30);
            this.FadeColour(ColourInfo.SingleColour(firstColor), 200);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(new Vector2(1f, 1f), 30);
            setColor();
            base.OnHoverLost(e);
        }

        private void updateColorWithStarRating(ValueChangedEvent<int> valueChangedEvent)
        {
            switch (valueChangedEvent.NewValue)
            {
                case 1:
                    firstColor = Colour4.LightGray;
                    secondColor = Colour4.Gray;
                    break;

                case 2:
                    firstColor = Colour4.WhiteSmoke;
                    secondColor = Colour4.White;
                    break;

                case 3:
                    firstColor = Colour4.WhiteSmoke;
                    secondColor = Colour4.Aquamarine;
                    break;

                case 4:
                    firstColor = Colour4.FromHex("#FDBAF4");
                    secondColor = Colour4.FromHex("#ECA5E2");
                    break;

                case 5:
                    firstColor = Colour4.FromHex("#FDF9BA");
                    secondColor = Colour4.FromHex("#F5EE77");
                    break;
            }

            setColor();
        }

        private void setColor() { this.FadeColour(ColourInfo.GradientVertical(firstColor, secondColor), 200); }
    }
}
