using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity.Drawables;
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

        private readonly SpriteText moneyText;

        private readonly EntityInfoListContainer itemContainer;

        private ItemDisplay itemInfo;

        private bool displayingInfo = false;

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
                            Child = moneyText = new SpriteText
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Text = "$0",
                                Font = FontUsage.Default.With(size: 56),
                                Margin = new MarginPadding { Top = 20 }
                            }
                        },
                        new DrawSizePreservingFillContainer
                        {
                            Child = itemContainer = new EntityInfoListContainer
                            {
                                RelativePositionAxes = Axes.Y,
                                Y = 0.1f,
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                Size = new Vector2(1)
                            }
                        },
                        new DrawSizePreservingFillContainer
                        {
                            Child = itemInfo = new ItemDisplay
                            {
                                Size = new Vector2(0, 1),
                                Anchor = Anchor.CentreRight,
                                Origin = Anchor.CentreRight,
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
            charactersButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Characters; });
            artifactsButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Artifacts; });
            weaponsButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Weapons; });
            Hide();
        }

        private void changeState()
        {
            if (displayingInfo) DisplayInfo();

            itemContainer.ClearList();

            switch (displayingSection.Value)
            {
                case InventoryDisplay.Characters:
                    itemContainer.AddFromList(GameData.Characters);
                    break;

                case InventoryDisplay.Artifacts:
                    itemContainer.AddFromList(GameData.Artifacts);
                    break;

                case InventoryDisplay.Weapons:
                    itemContainer.AddFromList(GameData.Weapons);
                    break;
            }
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

        public void DisplayInfo()
        {
            displayingInfo = !displayingInfo;

            switch (displayingInfo)
            {
                case true:
                    itemContainer.ResizeTo(new Vector2(0.5f, 1), 100);
                    itemInfo.ResizeTo(new Vector2(0.5f, 1), 100);
                    moneyText.MoveToX(-100f, 100);
                    break;

                case false:
                    itemContainer.ResizeTo(new Vector2(1), 100);
                    itemInfo.ResizeTo(new Vector2(0, 1), 100);
                    moneyText.MoveToX(0f, 100);
                    break;
            }
        }

        public override void Show()
        {
            isShowing = true;
            moneyText.Text = $"${GameData.Money.Amount}";
            base.Show();
            topButtons.MoveToY(0, FADE_TIME, Easing.InOutCubic);
            itemContainerBox.FadeIn(FADE_TIME, Easing.InOutCubic);
            displayingSection.Value = InventoryDisplay.Characters;
        }

        public override void Hide()
        {
            if (displayingInfo) DisplayInfo();
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
