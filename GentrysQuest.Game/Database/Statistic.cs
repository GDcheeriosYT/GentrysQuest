using osu.Framework.Logging;

namespace GentrysQuest.Game.Database
{
    public class Statistic : IStatistic
    {
        public Statistic(StatTypes statType, short scoreReward = 0)
        {
            Logger.Log($"{statType.ToString()} Created");
            ScoreReward = scoreReward;
        }

        public string Name { get; }
        public StatTypes StatType { get; }
        public int Value { get; protected set; } = 0;
        public short ScoreReward { get; }
        public bool IsConsecutive { get; protected set; } = false;
        public virtual void Add(int amount) => Value += amount;

        public string Summary()
        {
            return $"{Name}: {Value}";
        }

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
