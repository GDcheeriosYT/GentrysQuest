using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class SwapItemButton : CompositeComponent
    {
        private Action clickAction;

        public SwapItemButton()
        {
            RelativeSizeAxes = Axes.None;
            Size = new Vector2(24);
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    CornerExponent = 2,
                    CornerRadius = 6,
                    BorderColour = Colour4.Black,
                    BorderThickness = 2,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Colour4.Gray,
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.75f),
                            Icon = FontAwesome.Solid.Recycle,
                            Colour = Colour4.Black
                        }
                    }
                }
            };
        }

        public void SetClickAction(Action action) => clickAction = action;

        protected override bool OnClick(ClickEvent e)
        {
            clickAction?.Invoke();
            return base.OnClick(e);
        }
    }
}
