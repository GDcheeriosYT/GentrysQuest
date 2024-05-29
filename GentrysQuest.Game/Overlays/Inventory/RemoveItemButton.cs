using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class RemoveItemButton : CompositeComponent
    {
        private Action clickAction;

        public RemoveItemButton()
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
                            Colour = Colour4.Red,
                            RelativeSizeAxes = Axes.Both
                        },
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Child = new SpriteText
                            {
                                Text = "X",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = Colour4.Black,
                                Font = FontUsage.Default.With(size: 32)
                            }
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
