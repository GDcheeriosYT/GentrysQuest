using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryReverseButton : InnerInventoryButton
    {
        private readonly SpriteIcon directionIcon;
        private int direction;
        public bool Reversed { get; private set; }

        public InventoryReverseButton()
        {
            AddInternal(directionIcon = new SpriteIcon
            {
                Icon = FontAwesome.Solid.ArrowUp,
                RelativePositionAxes = Axes.Both,
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(0.7f)
            });
        }

        protected override bool OnHover(HoverEvent e)
        {
            directionIcon.RotateTo(direction + 35, 50);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            directionIcon.RotateTo(direction, 50);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Reversed = !Reversed;
            direction = Reversed ? 180 : 0;
            directionIcon.RotateTo(direction, 50);
            return base.OnClick(e);
        }
    }
}
