using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
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
        private readonly SpriteText experienceRequirementText;
        private readonly ProgressBar experienceBar;
        private readonly InventoryLevelUpBox inventoryLevelUpBox;
        private readonly StarRatingContainer starRatingContainer;
        private readonly Sprite entityDisplay;
        private readonly Container entityAttributeContainer;
        private readonly InventoryButton levelUpButton;
        private TextureStore textureStore;

        public ItemDisplay()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
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
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1, 0),
                                    Position = new Vector2(0, 30),
                                    Children = new Drawable[]
                                    {
                                        starRatingContainer = new StarRatingContainer(1)
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Size = new Vector2(0, 80)
                                        },
                                        new Container
                                        {
                                            Size = new Vector2(140, 40),
                                            Masking = true,
                                            EdgeEffect = new EdgeEffectParameters
                                            {
                                                Type = EdgeEffectType.Shadow,
                                                Colour = new Colour4(0, 0, 0, 180),
                                                Radius = 20,
                                                Roundness = 3
                                            },
                                            Anchor = Anchor.CentreRight,
                                            Origin = Anchor.CentreRight,
                                            Children = new Drawable[]
                                            {
                                                levelText = new SpriteText
                                                {
                                                    Anchor = Anchor.CentreRight,
                                                    Origin = Anchor.CentreRight,
                                                    Text = "",
                                                    Font = FontUsage.Default.With(size: 32),
                                                    AllowMultiline = false,
                                                    RelativeSizeAxes = Axes.Both
                                                }
                                            }
                                        },
                                        experienceBar = new ProgressBar(0, 1)
                                        {
                                            RelativeSizeAxes = Axes.None,
                                            Size = new Vector2(140, 5),
                                            Position = new Vector2(0, 10),
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            ForegroundColour = Colour4.DarkBlue
                                        },
                                        experienceRequirementText = new SpriteText
                                        {
                                            Text = "",
                                            Position = new Vector2(0, 20),
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                        }
                                    }
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
                                }
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

                        entityAttributeContainer = new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.Both,
                        },
                        levelUpButton = new InventoryButton("Levelus Uppus")
                        {
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.9f, 0.2f),
                            Position = new Vector2(-5, -5),
                            Scale = new Vector2(0.5f),
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight
                        },
                        inventoryLevelUpBox = new InventoryLevelUpBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.9f, 0.2f),
                            Position = new Vector2(5, -5),
                            Scale = new Vector2(0.5f),
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft
                        }
                    }
                }
            };
            Masking = true;
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
            updateExperienceBar(entity);
            levelUpButton.SetAction(delegate
            {
                entity.AddXp(inventoryLevelUpBox.GetAmount() * 10);
                updateExperienceBar(entity);
            });
            starRatingContainer.starRating.Value = entity.StarRating.Value;
            entityDisplay.Texture = textureStore.Get(entity.TextureMapping.Get("Icon"));

            switch (entity)
            {
                case Character:
                    break;

                case Artifact:
                    break;

                case Weapon:
                    break;
            }
        }

        private void updateExperienceBar(EntityBase entity)
        {
            levelText.Text = entity.Experience.Level.ToString();
            experienceRequirementText.Text = entity.Experience.Xp.ToString();
            experienceBar.Min = 0;
            experienceBar.Current = entity.Experience.Xp.Current.Value;
            experienceBar.Max = entity.Experience.Xp.Requirement.Value;
        }
    }
}
