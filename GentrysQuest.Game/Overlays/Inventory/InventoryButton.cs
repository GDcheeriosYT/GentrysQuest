using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryButton : CompositeDrawable
    {
        private SpriteText buttonText;
        private Action action;

        public InventoryButton(string text)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Masking = true;
            CornerExponent = 2;
            CornerRadius = 10;
            BorderColour = Colour4.Black;
            BorderThickness = 4;
            Size = new Vector2(1, 142);
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(136, 136, 136, 255)
                },
                buttonText = new SpriteText()
                {
                    Text = text,
                    Colour = Colour4.Black,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 48)
                }
            };
        }

        public void SetAction(Action action) => this.action = action;

        protected override bool OnClick(ClickEvent e)
        {
            action.Invoke();
            return base.OnClick(e);
        }
    }
}
