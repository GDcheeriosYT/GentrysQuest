using GentrysQuest.Game.Database;
using osu.Framework.Graphics;
using osuTK.Input;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class InventoryLevelUpBox : InventoryButton
    {
        private int amount;

        public InventoryLevelUpBox()
            : base("$0")
        {
            SetAction(delegate
            {
                int actionAmount = 1;
                if (Keyboard.GetState().IsKeyDown(Key.ControlLeft)) actionAmount *= 10;
                if (Keyboard.GetState().IsKeyDown(Key.ShiftLeft)) actionAmount *= 100;
                if (Keyboard.GetState().IsKeyDown(Key.AltLeft)) actionAmount = -actionAmount;

                if (amount + actionAmount >= 0) amount += actionAmount;
                else amount = 0;
                SetTextColor(Colour4.Red);
                if (GameData.Money.CanAfford(amount)) SetTextColor(Colour4.Black);
                SetText($"${amount.ToString()}");
            });
        }

        public int GetAmount() => amount;
    }
}
