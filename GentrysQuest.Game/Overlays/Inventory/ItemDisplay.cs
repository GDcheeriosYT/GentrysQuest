using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class ItemDisplay : CompositeDrawable
    {
        private EntityBase entity;

        public ItemDisplay()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Masking = true;
            CornerExponent = 2;
            CornerRadius = 15;
            InternalChildren = new Drawable[]
            {
            };
        }
    }
}
