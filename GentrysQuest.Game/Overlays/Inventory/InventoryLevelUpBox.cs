using GentrysQuest.Game.Database;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryLevelUpBox() : InventoryButton("$0")
    {
        private int amount;
        private bool multiplyTen = false;
        private bool multiplyHundred = false;

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            int actionAmount = 1;
            if (multiplyTen) actionAmount *= 10;
            if (multiplyHundred) actionAmount *= 100;

            switch (e.Button)
            {
                case MouseButton.Left:
                    break;

                case MouseButton.Right:
                    actionAmount = -actionAmount;
                    break;
            }

            if (amount + actionAmount >= 0) amount += actionAmount;
            else amount = 0;
            SetTextColor(Colour4.Red);
            if (GameData.Money.CanAfford(amount)) SetTextColor(Colour4.Black);
            SetText($"${amount.ToString()}");
            return base.OnMouseDown(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.ControlLeft:
                    multiplyTen = true;
                    break;

                case Key.ShiftLeft:
                    multiplyHundred = true;
                    break;
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            switch (e.Key)
            {
                case Key.ControlLeft:
                    multiplyTen = false;
                    break;

                case Key.ShiftLeft:
                    multiplyHundred = false;
                    break;
            }

            base.OnKeyUp(e);
        }

        public int GetAmount() => amount;
    }
}
