using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryOverlay : CompositeDrawable
    {
        /// <summary>
        /// The time it takes to fade
        /// </summary>
        private const int FADE_TIME = 300;

        /// <summary>
        /// if the inventory is being displayed
        /// </summary>
        private bool isShowing = false;

        /// <summary>
        /// The section being displayed in the inventory
        /// </summary>
        private readonly Bindable<InventoryDisplay> displayingSection = new Bindable<InventoryDisplay>(InventoryDisplay.Hidden);

        private readonly DrawSizePreservingFillContainer topButtons;

        private readonly InventoryButton charactersButton;
        private readonly InventoryButton artifactsButton;
        private readonly InventoryButton weaponsButton;
        private readonly InventoryButton exitButton;

        private readonly DrawSizePreservingFillContainer itemContainerBox;

        private readonly FillFlowContainer inventoryTop;

        private readonly SpriteText moneyText;

        private readonly EntityInfoListContainer itemContainer;

        private readonly InventoryReverseButton reverseButton;

        private readonly InnerInventoryButton sortButton;

        private readonly string[] sortTypes = new[] { "Star Rating", "Name", "Level" };
        private int sortIndexCounter = 0;

        private ItemDisplay itemInfo;

        private bool displayingInfo;
        private SelectionModes selectionMode = SelectionModes.Single;
        private Character equippingToCharacter;
        private int? artifactSelectionIndex;

        public InventoryOverlay()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Depth = -3;
            InternalChildren = new Drawable[]
            {
                topButtons = new DrawSizePreservingFillContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0),
                    Size = new Vector2(0.98f, 200),
                    Child = new FillFlowContainer<InventoryButton>
                    {
                        Direction = FillDirection.Horizontal,
                        RelativeSizeAxes = Axes.Both,
                        RelativePositionAxes = Axes.Both,
                        FillMode = FillMode.Stretch,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(0.2f, 1),
                        Spacing = new Vector2(40),
                        Children = new[]
                        {
                            charactersButton = new InventoryButton("Characters"),
                            artifactsButton = new InventoryButton("Artifacts"),
                            weaponsButton = new InventoryButton("Weapons")
                        }
                    }
                },
                itemContainerBox = new DrawSizePreservingFillContainer
                {
                    Masking = true,
                    CornerExponent = 2,
                    CornerRadius = 20,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(0.7f, 0.78f),
                    Position = new Vector2(0, -0.01f),
                    Margin = new MarginPadding { Vertical = 10 },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Colour4(0, 0, 0, 185)
                        },
                        new DrawSizePreservingFillContainer
                        {
                            Children = new Drawable[]
                            {
                                moneyText = new SpriteText
                                {
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Text = "$0",
                                    Font = FontUsage.Default.With(size: 56),
                                    Margin = new MarginPadding { Top = 20, Left = 50 }
                                },
                                new FillFlowContainer
                                {
                                    Direction = FillDirection.Horizontal,
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                    Margin = new MarginPadding { Top = 20, Right = 50 },
                                    Children = new Drawable[]
                                    {
                                        reverseButton = new InventoryReverseButton
                                        {
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Margin = new MarginPadding { Left = 12 },
                                            Size = new Vector2(46)
                                        },
                                        sortButton = new InnerInventoryButton(new SpriteText
                                        {
                                            Text = sortTypes[sortIndexCounter],
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre
                                        })
                                        {
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Size = new Vector2(200, 46),
                                        }
                                    }
                                }
                            }
                        },
                        new DrawSizePreservingFillContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = itemContainer = new EntityInfoListContainer
                            {
                                RelativePositionAxes = Axes.Y,
                                Y = 0.1f,
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                Size = new Vector2(1, 0.2f)
                            }
                        },
                        new DrawSizePreservingFillContainer
                        {
                            Child = itemInfo = new ItemDisplay(this)
                            {
                                Size = new Vector2(0, 0.9f),
                                Y = 0.1f,
                                Anchor = Anchor.TopRight,
                                Origin = Anchor.TopRight,
                            }
                        }
                    }
                },
            };
            Origin = Anchor.Centre;
            itemContainer.AddFromList(GameData.Characters);
            displayingSection.BindValueChanged(_ =>
            {
                changeState();
            });
            charactersButton.SetAction(() => { swapCategory(InventoryDisplay.Characters); });
            artifactsButton.SetAction(() => { swapCategory(InventoryDisplay.Artifacts); });
            weaponsButton.SetAction(() => { swapCategory(InventoryDisplay.Weapons); });
            sortButton.OnClickEvent += delegate
            {
                sortButton.Text.Text = HelpMe.GetNextValueFromArray(sortTypes, ref sortIndexCounter);
                itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed);
            };
            reverseButton.OnClickEvent += delegate { itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed); };
            itemContainer.FinishedLoading += delegate { itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed); };
            itemContainer.FinishedLoading += delegate
            {
                switch (selectionMode)
                {
                    case SelectionModes.Single:
                        foreach (EntityInfoDrawable entityInfoDrawable in itemContainer.GetEntityInfoDrawables())
                        {
                            entityInfoDrawable.OnClickEvent += delegate
                            {
                                foreach (EntityInfoDrawable entityInfoDrawable2 in itemContainer.GetEntityInfoDrawables())
                                {
                                    if (entityInfoDrawable != entityInfoDrawable2) entityInfoDrawable2.Unselect();
                                }

                                unDisplayInfo();
                                if (entityInfoDrawable.IsSelected) displayInfo(entityInfoDrawable);

                                switch (entityInfoDrawable.entity)
                                {
                                    case Character character:
                                        equippingToCharacter = (Character)entityInfoDrawable.entity;
                                        break;
                                }
                            };
                        }

                        break;

                    case SelectionModes.Multi:
                        break;

                    case SelectionModes.Equipping:
                        foreach (EntityInfoDrawable entityInfoDrawable in itemContainer.GetEntityInfoDrawables())
                        {
                            entityInfoDrawable.OnClickEvent += delegate
                            {
                                entityInfoDrawable.Unselect();

                                if (artifactSelectionIndex == null)
                                {
                                    Weapon weapon = (Weapon)entityInfoDrawable.entity;
                                    Weapon? weaponFromCharacter = equippingToCharacter.Weapon;
                                    equippingToCharacter.SetWeapon(weapon);
                                    if (weaponFromCharacter != null) GameData.Weapons.Add(weaponFromCharacter);
                                    GameData.Weapons.Remove(weapon);
                                }
                                else
                                {
                                    Artifact artifact = (Artifact)entityInfoDrawable.entity;
                                    Artifact? artifactFromCharacter = equippingToCharacter.Artifacts.Get((int)artifactSelectionIndex);
                                    equippingToCharacter.Artifacts.Equip(artifact, (int)artifactSelectionIndex);
                                    if (artifactFromCharacter != null) GameData.Artifacts.Add(artifactFromCharacter);
                                    GameData.Artifacts.Remove(artifact);
                                }

                                swapCategory(InventoryDisplay.Characters);
                                Scheduler.AddDelayed(() => itemInfo.DisplayItem(equippingToCharacter), 1500);
                            };
                        }

                        break;
                }
            };
            GameData.Money.Amount.ValueChanged += delegate { moneyText.Text = $"${GameData.Money.Amount}"; };
            Hide();
        }

        private void changeState()
        {
            itemContainer.ClearList();
            unDisplayInfo();

            switch (displayingSection.Value)
            {
                case InventoryDisplay.Characters:
                    itemContainer.AddFromList(GameData.Characters);
                    itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed);
                    break;

                case InventoryDisplay.Artifacts:
                    itemContainer.AddFromList(GameData.Artifacts);
                    itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed);
                    break;

                case InventoryDisplay.Weapons:
                    itemContainer.AddFromList(GameData.Weapons);
                    itemContainer.Sort(sortTypes[sortIndexCounter], reverseButton.Reversed);
                    break;
            }
        }

        private void swapCategory(InventoryDisplay inventoryDisplay)
        {
            displayingSection.Value = inventoryDisplay;
            selectionMode = SelectionModes.Single;
        }

        public void SetWeapon()
        {
            displayingSection.Value = InventoryDisplay.Weapons;
            selectionMode = SelectionModes.Equipping;
        }

        public void SetArtifact(int index)
        {
            displayingSection.Value = InventoryDisplay.Artifacts;
            selectionMode = SelectionModes.Equipping;
            artifactSelectionIndex = index;
        }

        public void ToggleDisplay()
        {
            switch (isShowing)
            {
                case true:
                    Hide();
                    break;

                case false:
                    Show();
                    break;
            }
        }

        private void displayInfo(EntityInfoDrawable entityInfoDrawable)
        {
            displayingInfo = true;
            itemContainer.ResizeTo(new Vector2(0.5f, 0.9f), 100);
            itemInfo.ResizeTo(new Vector2(0.5f, 0.9f), 100);
            itemInfo.DisplayItem(entityInfoDrawable.entity);
        }

        private void unDisplayInfo()
        {
            itemContainer.ResizeTo(new Vector2(1, 0.9f), 100);
            itemInfo.ResizeTo(new Vector2(0, 0.9f), 100);
            itemInfo.FadeOut(100);
            displayingInfo = false;
        }

        public override void Show()
        {
            isShowing = true;
            base.Show();
            topButtons.MoveToY(0, FADE_TIME, Easing.InOutCubic);
            itemContainerBox.FadeIn(FADE_TIME, Easing.InOutCubic);
            displayingSection.Value = InventoryDisplay.Characters;
        }

        public override void Hide()
        {
            unDisplayInfo();
            isShowing = false;
            topButtons.MoveToY(-2, FADE_TIME, Easing.InOutCubic);
            itemContainer.ClearList();
            displayingSection.Value = InventoryDisplay.Hidden;
            itemContainerBox.FadeOut(FADE_TIME, Easing.InOutCubic);
            Scheduler.AddDelayed(() =>
            {
                base.Hide();
            }, FADE_TIME);
        }
    }
}
