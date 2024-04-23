using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryOverlay : CompositeDrawable
    {
        private Inventory inventory;

        private Container topButtons;
        private

        public InventoryOverlay(Inventory inventory)
        {
            this.inventory = inventory;

            InternalChildren = new Drawable[]
            {

            };
        }
    }
}
