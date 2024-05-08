using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Entity.Weapon;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class ItemDisplay : CompositeDrawable
    {
        private EntityBase entity;
        private readonly SpriteText nameText;
        private readonly SpriteText descriptionText;
        private readonly SpriteText levelText;
        private readonly StarRatingContainer starRatingContainer;
        private readonly Sprite entityDisplay;
        private readonly Container entityAttributeContainer;
        private TextureStore textureStore;

        public ItemDisplay()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Masking = true;
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(1, 80),
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.Both,
                            Colour = ColourInfo.GradientHorizontal(Colour4.Gray, new Colour4(255, 255, 255, 0)),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativePositionAxes = Axes.Both,
                                    RelativeSizeAxes = Axes.Both
                                },
                                new Box
                                {
                                    Colour = Colour4.Black,
                                    RelativePositionAxes = Axes.Both,
                                    RelativeSizeAxes = Axes.X,
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    Size = new Vector2(1, 3)
                                },
                                new Box
                                {
                                    Colour = Colour4.Black,
                                    RelativePositionAxes = Axes.Both,
                                    RelativeSizeAxes = Axes.X,
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                    Size = new Vector2(1, 3)
                                }
                            }
                        },
                        nameText = new SpriteText
                        {
                            Text = "",
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = FontUsage.Default.With(size: 64),
                            RelativeSizeAxes = Axes.X,
                            Width = 1,
                            Truncate = true,
                            AllowMultiline = false,
                            Padding = new MarginPadding { Left = 2 }
                        },
                        new Container
                        {
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Position = new Vector2(0, 80),
                            Size = new Vector2(1, 420),
                            RelativeSizeAxes = Axes.X,
                            Children = new Drawable[]
                            {
                                entityDisplay = new Sprite
                                {
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Size = new Vector2(1),
                                    RelativeSizeAxes = Axes.Both
                                },
                                starRatingContainer = new StarRatingContainer(1)
                                {
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Size = new Vector2(0.75f, 80),
                                    RelativeSizeAxes = Axes.X,
                                },
                                descriptionText = new SpriteText
                                {
                                    Text = "",
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Font = FontUsage.Default.With(size: 24),
                                    Position = new Vector2(0, 120),
                                    RelativeSizeAxes = Axes.X,
                                    Width = 1f,
                                    AllowMultiline = true,
                                    Padding = new MarginPadding { Left = 5 }
                                },
                            }
                        }
                    }
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(1, 0.5f),
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.BottomCentre,
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.X,
                            Colour = ColourInfo.GradientVertical(new Colour4(0, 0, 26, 0), new Colour4(0, 0, 0, 155)),
                            Size = new Vector2(1, 100)
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.LightGray
                        },
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1, 50),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Colour4.Gray
                                },
                                new Box
                                {
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                    RelativeSizeAxes = Axes.X,
                                    Size = new Vector2(1, 3),
                                    Colour = Colour4.DarkGray
                                },
                                new Box
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.X,
                                    Size = new Vector2(1, 3),
                                    Colour = Colour4.DarkGray
                                },
                                new SpriteText
                                {
                                    Text = "Details",
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                }
                            }
                        },
                        levelText = new SpriteText
                        {
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Text = "",
                            Position = new Vector2(10, 60),
                            Font = FontUsage.Default.With(size: 32),
                            Colour = Colour4.SandyBrown
                        },
                        entityAttributeContainer = new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            this.textureStore = textureStore;
        }

        public void DisplayItem(EntityBase entity)
        {
            this.FadeIn(50);
            nameText.Text = entity.Name;
            descriptionText.Text = entity.Description;
            levelText.Text = entity.Experience.ToString();
            starRatingContainer.starRating.Value = entity.StarRating.Value;
            entityDisplay.Texture = textureStore.Get(entity.TextureMapping.Get("Icon"));

            switch (entity)
            {
                case Character:
                    entityAttributeContainer.Add(new FillFlowContainer
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        AutoSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {

                        }
                    });
                    break;

                case Artifact:
                    break;

                case Weapon:
                    break;
            }
        }
    }
}
