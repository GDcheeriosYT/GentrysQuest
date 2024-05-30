using System;
using GentrysQuest.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryButton : GQButton
    {
        private SpriteText buttonText;
        private Action action;

        public InventoryButton(string text)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
            CornerExponent = 2;
            CornerRadius = 10;
            BorderColour = Colour4.Black;
            BorderThickness = 2;
            Size = new Vector2(200, 100);
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(136, 136, 136, 255)
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Child = buttonText = new SpriteText()
                    {
                        Text = text,
                        Colour = Colour4.Black,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 36)
                    }
                }
            };
        }

        public void SetText(string text) => buttonText.Text = text;

        public void SetTextColor(Colour4 colour) => buttonText.Colour = colour;
    }
}
