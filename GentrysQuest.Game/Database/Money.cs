using osu.Framework.Bindables;

namespace GentrysQuest.Game.Database
{
    public class Money(int amount = 0)
    {
        public Bindable<int> Amount { get; private set; } = new(amount);

        public bool CanAfford(int amount) => this.Amount.Value >= amount;

        public void Spend(int amount) => this.Amount.Value -= amount;

        public void Hand(int amount) => this.Amount.Value += amount;
    }
}
