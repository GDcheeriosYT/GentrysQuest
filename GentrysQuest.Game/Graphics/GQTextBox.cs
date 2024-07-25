using osu.Framework.Allocation;
using osu.Framework.Graphics.UserInterface;
using osuTK.Graphics;

namespace GentrysQuest.Game.Graphics
{
    public partial class GQTextBox : BasicTextBox
    {
        protected override float LeftRightPadding => 10;
        protected override float CaretWidth => 5;

        [BackgroundDependencyLoader]
        private void load()
        {
            BackgroundUnfocused = new Color4(0, 0, 0, 200);
            BackgroundFocused = new Color4(0, 0, 0, 255);
            BackgroundCommit = Color4.Aqua;
        }
    }
}
