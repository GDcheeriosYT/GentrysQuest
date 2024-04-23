using GentrysQuest.Game.Utils;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class StarRatingDrawable : SpriteIcon
    {
        public Bindable<bool> isEnabled = new Bindable<bool>();
        private LimitedInt indicator;

        public StarRatingDrawable(int indicator)
        {
            this.indicator = new LimitedInt(5, 1);
            this.indicator.Value = indicator;

            Icon = FontAwesome.Solid.Star;
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Colour = ColourInfo.GradientVertical(Colour4.DarkGray, Colour4.LightGray);
            Alpha = 0.5f;
            Size = new Vector2(1f);

            isEnabled.BindValueChanged(whenChanged, true);
        }

        private void whenChanged(ValueChangedEvent<bool> valueChangedEvent)
        {
            if (!valueChangedEvent.NewValue)
            {
                this.FadeColour(ColourInfo.GradientVertical(Colour4.DarkGray, Colour4.LightGray), 50);
                this.FadeTo(0.5f, 100);
            }
        }

        public void updateColour(ColourInfo color, int indication, int delay)
        {
            if (indication >= indicator.Value)
            {
                isEnabled.Value = true;
                this.Delay(delay).Then().FadeColour(color, 100);
                this.Delay(delay).Then().FadeTo(1, 100);
            }
            else isEnabled.Value = false;
        }
    }
}
