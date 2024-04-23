using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryButton : CompositeDrawable
    {
        private SpriteText buttonText;

        public InventoryButton(string text)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            CornerExponent = 2;
            CornerRadius = 5;
            BorderColour = Colour4.Black;
            BorderThickness = 1;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both
                }
            };
        }
    }
}
