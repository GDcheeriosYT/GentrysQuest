using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Overlays.UserMenu
{
    public partial class UserMenu : Container
    {
        private bool toggled = false;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.6f, 0.75f),
                    Origin = Anchor.Centre,
                    Position = new Vector2(0f, 0f),
                    Colour = Colour4.Gray,
                }
            };
        }

        public void Toggle()
        {
            toggled = !toggled;
            this.MoveToY(toggled ? 0f : 1.5f);
        }

        public void ToggleOn()
        {
            toggled = true;
            this.MoveToY(toggled ? 0f : 1.5f);
        }

        public void ToggleOff()
        {
            toggled = false;
            this.MoveToY(toggled ? 0f : 1.5f);
        }
    }
}
