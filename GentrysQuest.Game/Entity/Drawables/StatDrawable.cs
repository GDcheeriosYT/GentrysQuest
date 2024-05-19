using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class StatDrawable : CompositeDrawable
    {
        private const int DURATION = 500;
        public string Name { get; private set; }
        private Box backgroundBox;
        private SpriteText nameText;
        private SpriteText valueText;

        public StatDrawable(string name, float value, bool isMain)
        {
            Name = name;
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(0.98f, isMain ? 100 : 50);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Margin = new MarginPadding { Top = 3 };
            InternalChildren = new Drawable[]
            {
                backgroundBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(0, 0, 0, 180)
                },
                nameText = new SpriteText
                {
                    Text = name,
                    Margin = new MarginPadding { Left = 5 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
                valueText = new SpriteText
                {
                    Text = "" + value,
                    Anchor = Anchor.CentreRight,
                    Margin = new MarginPadding { Right = 20 },
                    Origin = Anchor.CentreRight
                }
            };

            if (isMain)
            {
                int size = 52;
                nameText.Font = FontUsage.Default.With(size: size);
                valueText.Font = FontUsage.Default.With(size: size);
            }

            this.FadeOut();
            this.FlashColour(Colour4.White, DURATION);
            this.FadeIn(DURATION);
        }

        public void UpdateValue(float value)
        {
            backgroundBox.FlashColour(new Colour4(255, 255, 255, 100), DURATION);
            valueText.Text = "" + value;
            valueText.FlashColour(Colour4.Gold, DURATION * 0.5);
            nameText.FlashColour(Colour4.Gold, DURATION * 0.5);
        }
    }
}
