using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class ExitButton : CompositeDrawable
    {
        private InventoryOverlay parent;

        public ExitButton(InventoryOverlay parent)
        {
            this.parent = parent;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black,
                    Alpha = 0.2f
                },
                new SpriteText
                {
                    Text = "Exit",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            parent.ToggleDisplay();
            return base.OnClick(e);
        }
    }
}
