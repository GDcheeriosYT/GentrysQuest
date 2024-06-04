using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableStatusEffect : CompositeDrawable
    {
        private readonly TextFlowContainer textFlowContainer;

        public DrawableStatusEffect(StatusEffect statusEffect)
        {
            Origin = Anchor.CentreLeft;
            Size = new Vector2(16);
            Margin = new MarginPadding
            {
                Left = 3
            };
            InternalChildren = new Drawable[]
            {
                new SpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = statusEffect.EffectColor,
                    Icon = statusEffect.Icon
                },
                textFlowContainer = new TextFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.Centre,
                }
            };
            textFlowContainer.AddText(new SpriteText
            {
                Text = statusEffect.Stack.ToString(),
                Font = FontUsage.Default.With(size: 12)
            });
            textFlowContainer.AddText(new SpriteText
            {
                Text = "x",
                Font = FontUsage.Default.With(size: 8)
            });
        }
    }
}
