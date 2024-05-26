using System;
using GentrysQuest.Game.Database;
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
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class ItemDisplay : CompositeDrawable
    {
        private EntityBase entity;
        private readonly InventoryOverlay inventoryReference;
        private readonly SpriteText nameText;
        private readonly SpriteText descriptionText;
        private readonly SpriteText levelText;
        private readonly SpriteText experienceRequirementText;
        private readonly ProgressBar experienceBar;
        private readonly InventoryLevelUpBox inventoryLevelUpBox;
        private readonly InventoryButton levelUpButton;
        private readonly InventoryButton exchangeButton;
        private readonly StarRatingContainer starRatingContainer;
        private readonly StatDrawableContainer statDrawableContainer;
        private readonly Container characterAttributesContainer;
        private EquipIcon artifactIcon1;
        private EquipIcon artifactIcon2;
        private EquipIcon artifactIcon3;
        private EquipIcon artifactIcon4;
        private EquipIcon artifactIcon5;
        private EquipIcon weaponIcon;
        private Character characterInfo;
        private Artifact artifactInfo;
        private Weapon weaponInfo;
        private readonly Sprite entityDisplay;
        private TextureStore textureStore;

        #region DesignProperties

        private const int LEVEL_UP_BUTTON_WIDTH = 420;
        private const int LEVEL_UP_BUTTON_HEIGHT = 50;

        #endregion

        public ItemDisplay(InventoryOverlay inventoryReference)
        {
            this.inventoryReference = inventoryReference;
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
                        statDrawableContainer = new StatDrawableContainer
                        {
                            Size = new Vector2(1, 0.65f),
                            Position = new Vector2(0, 60)
                        },
                        characterAttributesContainer = new Container
                        {
                            Masking = true,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f, 0.65f),
                            Position = new Vector2(0, 60),
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Children = new Drawable[]
                            {
                                new SpriteText
                                {
                                    Text = "Equips",
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    Font = FontUsage.Default.With(size: 36)
                                },
                                new BasicScrollContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1, 0.8f),
                                    Position = new Vector2(0, 32),
                                    ClampExtension = 1,
                                    ScrollbarVisible = false,
                                    Child = new FillFlowContainer
                                    {
                                        Y = 0,
                                        Direction = FillDirection.Vertical,
                                        AutoSizeAxes = Axes.Y,
                                        Anchor = Anchor.TopCentre,
                                        Origin = Anchor.TopCentre,
                                        Children = new Drawable[]
                                        {
                                            weaponIcon = new EquipIcon(null),
                                            new Box
                                            {
                                                Size = new Vector2(120, 4),
                                                Origin = Anchor.Centre,
                                                Colour = Colour4.DarkGray
                                            },
                                            artifactIcon1 = new EquipIcon(null),
                                            artifactIcon2 = new EquipIcon(null),
                                            artifactIcon3 = new EquipIcon(null),
                                            artifactIcon4 = new EquipIcon(null),
                                            artifactIcon5 = new EquipIcon(null),
                                        }
                                    }
                                }
                            }
                        },
                        new FillFlowContainer
                        {
                            Size = new Vector2(0, 60),
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Direction = FillDirection.Horizontal,
                            AutoSizeAxes = Axes.X,
                            Spacing = new Vector2(20),
                            Children = new Drawable[]
                            {
                                exchangeButton = new InventoryButton("Infuse")
                                {
                                    Size = new Vector2(LEVEL_UP_BUTTON_WIDTH, LEVEL_UP_BUTTON_HEIGHT),
                                },
                                inventoryLevelUpBox = new InventoryLevelUpBox()
                                {
                                    Size = new Vector2(LEVEL_UP_BUTTON_WIDTH, LEVEL_UP_BUTTON_HEIGHT),
                                },
                                levelUpButton = new InventoryButton("Upgrade")
                                {
                                    Size = new Vector2(LEVEL_UP_BUTTON_WIDTH, LEVEL_UP_BUTTON_HEIGHT),
                                },
                            }
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
            characterInfo = null;
            artifactInfo = null;
            weaponInfo = null;
            this.FadeIn(50);
            nameText.Text = entity.Name;
            descriptionText.Text = entity.Description;
            updateExperienceBar(entity);
            levelUpButton.SetAction(delegate
            {
                int amount = inventoryLevelUpBox.GetAmount();

                if (GameData.Money.CanAfford(amount))
                {
                    entity.AddXp(amount * 10);
                    GameData.Money.Spend(amount);
                }

                updateExperienceBar(entity);
            });
            starRatingContainer.starRating.Value = entity.StarRating.Value;
            entityDisplay.Texture = textureStore.Get(entity.TextureMapping.Get("Icon"));
            statDrawableContainer.Clear();
            characterAttributesContainer.Hide();

            switch (entity)
            {
                case Character character:
                    characterInfo = character;
                    characterAttributesContainer.Show();

                    exchangeButton.Hide();
                    inventoryLevelUpBox.Show();
                    resizeLevelUpComponents(2);

                    foreach (Stat stat in character.Stats.GetStats())
                    {
                        statDrawableContainer.AddStat(new StatDrawable(stat.Name, (float)stat.Total(), false));
                    }

                    statDrawableContainer.ResizeWidthTo(0.5f);

                    weaponIcon.SetEquip(character.Weapon);
                    weaponIcon.SetAction(delegate { inventoryReference.ClickWeapon(); });
                    weaponIcon.SetSwapAction(delegate { inventoryReference.SwapWeapon(); });
                    weaponIcon.SetRemoveAction(delegate { inventoryReference.RemoveWeapon(); });

                    artifactIcon1.SetEquip(character.Artifacts.Get(0));
                    artifactIcon1.SetAction(delegate { inventoryReference.ClickArtifact(0); });
                    artifactIcon1.SetSwapAction(delegate { inventoryReference.SwapArtifact(0); });
                    artifactIcon1.SetRemoveAction(delegate { inventoryReference.RemoveArtifact(0); });

                    artifactIcon2.SetEquip(character.Artifacts.Get(1));
                    artifactIcon2.SetAction(delegate { inventoryReference.ClickArtifact(1); });
                    artifactIcon2.SetSwapAction(delegate { inventoryReference.SwapArtifact(1); });
                    artifactIcon2.SetRemoveAction(delegate { inventoryReference.RemoveArtifact(1); });

                    artifactIcon3.SetEquip(character.Artifacts.Get(2));
                    artifactIcon3.SetAction(delegate { inventoryReference.ClickArtifact(2); });
                    artifactIcon3.SetSwapAction(delegate { inventoryReference.SwapArtifact(2); });
                    artifactIcon3.SetRemoveAction(delegate { inventoryReference.RemoveArtifact(2); });

                    artifactIcon4.SetEquip(character.Artifacts.Get(3));
                    artifactIcon4.SetAction(delegate { inventoryReference.ClickArtifact(3); });
                    artifactIcon4.SetSwapAction(delegate { inventoryReference.SwapArtifact(3); });
                    artifactIcon4.SetRemoveAction(delegate { inventoryReference.RemoveArtifact(3); });

                    artifactIcon5.SetEquip(character.Artifacts.Get(4));
                    artifactIcon5.SetAction(delegate { inventoryReference.ClickArtifact(4); });
                    artifactIcon5.SetSwapAction(delegate { inventoryReference.SwapArtifact(4); });
                    artifactIcon5.SetRemoveAction(delegate { inventoryReference.RemoveArtifact(4); });

                    characterInfo.OnUpdateStats -= updateCharacterStatContainer;
                    characterInfo.OnUpdateStats += updateCharacterStatContainer;
                    break;

                case Artifact artifact:
                    inventoryReference.FocusedArtifact = artifact;
                    artifactInfo = artifact;
                    Buff mainAttribute = artifact.MainAttribute;
                    exchangeButton.SetAction(inventoryReference.StartArtifactExchange);

                    inventoryLevelUpBox.Hide();
                    exchangeButton.Show();
                    levelUpButton.SetAction(delegate
                    {
                        inventoryReference.ExchangeArtifacts();
                        updateExperienceBar(entity);
                    });
                    resizeLevelUpComponents(2);
                    statDrawableContainer.AddStat(new StatDrawable(mainAttribute.StatType.ToString(), (float)mainAttribute.Value.Value, true));

                    foreach (Buff attribute in artifact.Attributes)
                    {
                        statDrawableContainer.AddStat(new StatDrawable(attribute.StatType.ToString(), (float)attribute.Value.Value, false));
                    }

                    statDrawableContainer.ResizeWidthTo(1f);

                    artifactInfo.OnLevelUp -= updateArtifactStatContainer;
                    artifactInfo.OnLevelUp += updateArtifactStatContainer;
                    break;

                case Weapon weapon:
                    inventoryReference.FocusedWeapon = weapon;
                    weaponInfo = weapon;

                    exchangeButton.SetAction(inventoryReference.StartWeaponExchange);
                    exchangeButton.Show();
                    inventoryLevelUpBox.Show();
                    levelUpButton.SetAction(delegate
                    {
                        if (GameData.Money.CanAfford(inventoryLevelUpBox.GetAmount()))
                        {
                            entity.AddXp(inventoryLevelUpBox.GetAmount() * 10);
                            GameData.Money.Spend(inventoryLevelUpBox.GetAmount());
                        }

                        inventoryReference.ExchangeWeapons();

                        updateExperienceBar(entity);
                    });
                    resizeLevelUpComponents(3);
                    statDrawableContainer.AddStat(new StatDrawable("Damage", (float)weapon.Damage.Total(), true));
                    statDrawableContainer.AddStat(new StatDrawable(weapon.Buff.StatType.ToString(), (float)weapon.Buff.Value.Value, false));
                    statDrawableContainer.ResizeWidthTo(1f);
                    weaponInfo.OnLevelUp -= updateWeaponStatContainer;
                    weaponInfo.OnLevelUp += updateWeaponStatContainer;
                    break;
            }
        }

        private void resizeLevelUpComponents(int amount)
        {
            if (amount < 2) amount = 2;
            exchangeButton.ResizeTo(new Vector2(LEVEL_UP_BUTTON_WIDTH / amount, LEVEL_UP_BUTTON_HEIGHT));
            inventoryLevelUpBox.ResizeTo(new Vector2(LEVEL_UP_BUTTON_WIDTH / amount, LEVEL_UP_BUTTON_HEIGHT));
            levelUpButton.ResizeTo(new Vector2(LEVEL_UP_BUTTON_WIDTH / amount, LEVEL_UP_BUTTON_HEIGHT));
        }

        private void updateExperienceBar(EntityBase entity)
        {
            levelText.Text = entity.Experience.Level.ToString();
            experienceRequirementText.Text = entity.Experience.Xp.ToString();
            experienceBar.Min = 0;
            experienceBar.Current = entity.Experience.Xp.Current.Value;
            experienceBar.Max = entity.Experience.Xp.Requirement.Value;
        }

        private void updateCharacterStatContainer()
        {
            try
            {
                foreach (Stat stat in characterInfo.Stats.GetStats())
                {
                    statDrawableContainer.GetStatDrawable(stat.Name).UpdateValue((float)stat.Total());
                }
            }
            catch (Exception)
            {
                Logger.Log("Yep, You already know (Character)");
            }
        }

        private void updateArtifactStatContainer()
        {
            try
            {
                statDrawableContainer.GetStatDrawable(artifactInfo.MainAttribute.StatType.ToString()).UpdateValue((float)artifactInfo.MainAttribute.Value.Value);

                foreach (Buff buff in artifactInfo.Attributes)
                {
                    StatDrawable statDrawable = statDrawableContainer.GetStatDrawable(buff.StatType.ToString());
                    if (statDrawable != null) statDrawable.UpdateValue((float)buff.Value.Value);
                    else statDrawableContainer.AddStat(new StatDrawable(buff.StatType.ToString(), (float)buff.Value.Value, false));
                }
            }
            catch (Exception)
            {
                Logger.Log("Yep, You already know (Artifact)");
            }
        }

        private void updateWeaponStatContainer()
        {
            try
            {
                statDrawableContainer.GetStatDrawable("Damage").UpdateValue((float)weaponInfo.Damage.Total());
                statDrawableContainer.GetStatDrawable(weaponInfo.Buff.StatType.ToString()).UpdateValue((float)weaponInfo.Buff.Value.Value);
            }
            catch (Exception)
            {
                Logger.Log("Yep, You already know (Weapon)");
            }
        }
    }
}
