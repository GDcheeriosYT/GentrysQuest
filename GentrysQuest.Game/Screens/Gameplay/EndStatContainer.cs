using System.Collections.Generic;
using GentrysQuest.Game.Database;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class EndStatContainer : CompositeDrawable
    {
        private readonly FillFlowContainer statHolder;
        private const int DELAY = 50;

        public EndStatContainer()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = statHolder = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        Direction = FillDirection.Vertical,
                        AutoSizeAxes = Axes.Y,
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre
                    }
                }
            };
        }

        public void PopulateStats(StatTracker statTracker)
        {
            List<IStatistic> statistics = statTracker.GetStats();

            for (int statIndex = 1; statIndex < statistics.Count; statIndex++)
            {
                var index = statIndex;
                Scheduler.AddDelayed(() =>
                {
                    addObject(statistics[index]);
                }, statIndex * DELAY);
            }
        }

        private void addObject(IStatistic statistic)
        {
            Container newContainer = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Alpha = 0,
                Size = new Vector2(0.8f, 82),
                Margin = new MarginPadding { Top = 5 },
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = new Colour4(25, 25, 25, 100)
                    },
                    new SpriteText
                    {
                        Text = statistic.Name,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Colour = Colour4.White,
                        Margin = new MarginPadding { Left = 5 }
                    },
                    new SpriteText
                    {
                        Text = statistic.Value.ToString(),
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Colour = Colour4.White,
                        Margin = new MarginPadding { Right = 5 }
                    }
                }
            };

            statHolder.Add(newContainer);
            newContainer.FadeIn(DELAY * 3).Then().FlashColour(Colour4.White, DELAY * 2);
        }
    }
}
