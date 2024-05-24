namespace GentrysQuest.Game.Database
{
    public class Statistic : IStatistic
    {
        public Statistic(StatTypes statType, short scoreReward = 0)
        {
            ScoreReward = scoreReward;
        }

        public string Name { get; }
        public StatTypes StatType { get; }
        public int Value { get; protected set; } = 0;
        public short ScoreReward { get; }
        public bool IsConsecutive { get; protected set; } = false;

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
