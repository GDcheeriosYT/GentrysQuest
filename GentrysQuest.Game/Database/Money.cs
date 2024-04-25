namespace GentrysQuest.Game.Database
{
    public class Money(int amount = 0)
    {
        public int Amount { get; private set; } = amount;

        public bool CanAfford(int amount) => this.Amount >= amount;

        public void Spend(int amount) => this.Amount -= amount;

        public void Hand(int amount) => this.Amount += amount;
    }
}
