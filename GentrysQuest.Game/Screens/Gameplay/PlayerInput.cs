using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class PlayerInput : CompositeDrawable
    {
        private readonly TextBox inputBox = new BasicTextBox
        {
            PlaceholderText = "Name",
            RelativeSizeAxes = Axes.Both,
            LengthLimit = 15
        };

        public PlayerInput()
        {
            AddInternal(inputBox);
        }

        public string GetName() => inputBox.Text;
    }
}
