using osu.Framework.Bindables;

namespace GentrysQuest.Game.Database
{
    public class Money(int amount = 0)
    {
        public bool InfiniteMoney = false;

        public Bindable<int> Amount { get; private set; } = new(amount);

        public bool CanAfford(int amount) => InfiniteMoney || Amount.Value >= amount;

        public void Spend(int amount)
        {
            if (!InfiniteMoney) Amount.Value -= amount;
        }

        public void Hand(int amount) => Amount.Value += amount;
    }
}
