using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class StatDrawableContainer : CompositeDrawable
    {
        private FillFlowContainer<StatDrawable> statDrawables;

        public StatDrawableContainer()
        {
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new SpriteText
                {
                    Text = "Stats",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 36)
                },
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Position = new Vector2(0, 32),
                    ClampExtension = 1,
                    ScrollbarVisible = false,
                    Child = statDrawables = new FillFlowContainer<StatDrawable>
                    {
                        Position = new Vector2(0, 32),
                        Direction = FillDirection.Vertical,
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X
                    }
                }
            };
        }

        public void AddStat(StatDrawable statDrawable) => statDrawables.Add(statDrawable);

        public StatDrawable GetStatDrawable(string name)
        {
            foreach (StatDrawable statDrawable in statDrawables.Children)
            {
                if (statDrawable.Name == name) return statDrawable;
            }

            return null;
        }

        public void Clear()
        {
            statDrawables.Clear();
        }
    }
}
