using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryOverlay : CompositeDrawable
    {
        /// <summary>
        /// The section being displayed in the inventory
        /// </summary>
        private Bindable<InventoryDisplay> displayingSection = new Bindable<InventoryDisplay>(InventoryDisplay.Characters);

        private InventoryButton charactersButton;
        private InventoryButton artifactsButton;
        private InventoryButton weaponsButton;

        private EntityInfoListContainer itemContainer;

        public InventoryOverlay()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                new DrawSizePreservingFillContainer
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
                        Size = new Vector2(0.3f, 1),
                        Spacing = new Vector2(40),
                        Children = new[]
                        {
                            charactersButton = new InventoryButton("Characters"),
                            artifactsButton = new InventoryButton("Artifacts"),
                            weaponsButton = new InventoryButton("Weapons")
                        }
                    },
                },
                new DrawSizePreservingFillContainer
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
                        itemContainer = new EntityInfoListContainer
                        {
                            RelativePositionAxes = Axes.Y,
                            Size = new Vector2(1)
                        }
                    }
                }
            };
            Origin = Anchor.Centre;
            itemContainer.AddFromList(GameData.Characters);
            displayingSection.BindValueChanged(_ =>
            {
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
            });
            charactersButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Characters; });
            artifactsButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Artifacts; });
            weaponsButton.SetAction(() => { displayingSection.Value = InventoryDisplay.Weapons; });
        }
    }
}
