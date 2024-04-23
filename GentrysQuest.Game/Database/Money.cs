namespace GentrysQuest.Game.Database
{
    public class Money(int amount = 0)
    {
        private int amount = amount;

        public bool CanAfford(int amount) => this.amount >= amount;

        public void Spend(int amount) => this.amount -= amount;

        public void Hand(int amount) => this.amount += amount;
    }
}
