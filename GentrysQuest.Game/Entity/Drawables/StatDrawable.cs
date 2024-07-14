using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    // TODO: Add way to display percentage
    public partial class StatDrawable : CompositeDrawable
    {
        private const int DURATION = 500;
        public string Identifier;
        private bool isPercent;
        public new string Name { get; private set; }
        private readonly Box backgroundBox;
        private readonly SpriteText nameText;
        private readonly SpriteText valueText;
        private readonly SpriteText changedToValue;
        private readonly SpriteIcon arrowIndicator;
        private readonly Box newIndicationBox;
        private readonly SpriteText newIndicationText;

        public StatDrawable(string name, float value, bool isMain, string identifier = null, bool isPercent = false)
        {
            this.isPercent = isPercent;
            Identifier = identifier ?? name;
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
                newIndicationBox = new Box
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Colour = Colour4.Gold,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1, 0.1f)
                },
                newIndicationText = new SpriteText
                {
                    Text = "new",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 16),
                    Colour = Colour4.Gold,
                },
                nameText = new SpriteText
                {
                    Text = name,
                    Margin = new MarginPadding { Left = 5 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    AutoSizeAxes = Axes.X,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Children = new Drawable[]
                    {
                        changedToValue = new SpriteText
                        {
                            Text = "" + value + percentText,
                            Anchor = Anchor.CentreRight,
                            Margin = new MarginPadding { Left = 20, Right = 20 },
                            Origin = Anchor.CentreRight
                        },
                        arrowIndicator = new SpriteIcon
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Icon = FontAwesome.Solid.LongArrowAltRight,
                            Colour = Colour4.Gray,
                            Size = new Vector2(64)
                        },
                        valueText = new SpriteText
                        {
                            Text = "" + value + percentText,
                            Anchor = Anchor.CentreRight,
                            Margin = new MarginPadding { Right = 20 },
                            Origin = Anchor.CentreRight
                        },
                    }
                }
            };

            if (isMain)
            {
                int size = 52;
                nameText.Font = FontUsage.Default.With(size: size);
                valueText.Font = FontUsage.Default.With(size: size);
                changedToValue.Font = FontUsage.Default.With(size: size);
            }

            newIndicationBox.Hide();
            newIndicationText.Hide();
            arrowIndicator.ScaleTo(new Vector2(0, 1));
            changedToValue.ScaleTo(new Vector2(0, 1));
        }

        /// <summary>
        /// Notification for new stat
        /// </summary>
        public void NewDisplay()
        {
            newIndicationBox.FadeIn(100);
            newIndicationText.FadeIn(100);
        }

        public void UpdateValue(float newValue)
        {
            if (valueText.Text == $"{newValue}{percentText}") return;

            valueText.Text = changedToValue.Text + percentText;
            backgroundBox.FlashColour(new Colour4(255, 255, 255, 100), DURATION);
            changedToValue.Text = "" + newValue + percentText;
            changedToValue.ScaleTo(1, DURATION * 0.2);
            arrowIndicator.ScaleTo(1, DURATION * 0.2);
            changedToValue.FadeColour(Colour4.Gold, DURATION * 0.2);
            valueText.FlashColour(Colour4.Gold, DURATION * 0.5);
            nameText.FlashColour(Colour4.Gold, DURATION * 0.5);
        }

        private string percentText => $"{(isPercent ? '%' : ' ')}";
    }
}
