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
        private SpriteText level;
        private TextFlowContainer mainInfoContainer;
        public StarRatingContainer starRatingContainer;
        public EntityBase entity;
        private Colour4 firstColor = Colour4.Gray;
        private Colour4 secondColor = Colour4.Gray;

        // [Resolved]
        // private TextureStore textures { get; }

        public EntityInfoDrawable(EntityBase entity)
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
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        icon = new EntityIconDrawable
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Margin = new MarginPadding(35),
                            Size = new Vector2(250)
                        },
                        new FillFlowContainer
                        {
                            Direction = FillDirection.Vertical,
                            AutoSizeAxes = Axes.Both,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Padding = new MarginPadding { Left = 5 },
                            Children = new Drawable[]
                            {
                                mainInfoContainer = new TextFlowContainer
                                {
                                    Size = new Vector2(400, 40)
                                },
                                starRatingContainer = new StarRatingContainer(entity.StarRating.Value)
                                {
                                    Size = new Vector2(200, 40)
                                }
                            }
                        }
                    }
                }
            };

            mainInfoContainer.AddText(name = new SpriteText
                {
                    Text = entity.Name,
                    Size = new Vector2(300, 35),
                    Font = FontUsage.Default.With(size: 38),
                    Truncate = true,
                    AllowMultiline = false
                }
            );
            mainInfoContainer.AddText(level = new SpriteText
            {
                Text = $"Level 0",
                Size = new Vector2(100, 35),
                Padding = new MarginPadding { Left = 10 },
                Font = FontUsage.Default.With(size: 24),
                AllowMultiline = false
            });
            starRatingContainer.starRating.BindValueChanged(updateColorWithStarRating, true);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            icon.Texture = textures.Get(entity.TextureMapping.Get("Icon"));
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(new Vector2(1.05f, 1f), 30);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(new Vector2(1f, 1f), 30);
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
