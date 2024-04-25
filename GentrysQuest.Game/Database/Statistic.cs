namespace GentrysQuest.Game.Database
{
    public class Statistic : IStatistic
    {
        public int Value { get; protected set; }
        public int ScoreValue { get; }
        public bool IsConsecutive { get; protected set; } = false;

        public Statistic()
        {
            Value = 0;
        }

        public void Add() => Value++;
        public void Add(int amount) => Value += amount;

        public void Result(IStatistic otherStatistic)
        {
            if (otherStatistic.IsConsecutive) Value = (Value > otherStatistic.Value) ? Value : otherStatistic.Value;
            else Value += otherStatistic.Value;
        }

        public bool Bigger(int amount)
        {
            return amount > Value;
        }
    }
}
