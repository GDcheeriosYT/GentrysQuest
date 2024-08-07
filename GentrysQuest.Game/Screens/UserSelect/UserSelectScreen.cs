using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK;

namespace GentrysQuest.Game.Screens.UserSelect
{
    public partial class UserSelectScreen : Screen
    {
        public UserSelectScreen()
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        AutoSizeAxes = Axes.Y,
                        Children = new Drawable[]
                        {
                            new FillFlowContainer
                            {
                                Y = -5,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.BottomCentre,
                                Direction = FillDirection.Horizontal,
                                AutoSizeAxes = Axes.Y,
                                Spacing = new Vector2(10),
                                Children = new Drawable[]
                                {
                                    new UserSelectTabButton(true),
                                    new UserSelectTabButton(false)
                                }
                            },
                            new Container
                            {
                                Size = new Vector2(600, 800),
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = Colour4.Gray
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
