using osu.Framework.Logging;

namespace GentrysQuest.Game.Database
{
    public class Statistic : IStatistic
    {
        public Statistic(StatTypes statType, short scoreReward = 0)
        {
            Logger.Log($"{statType.ToString()} Created");
            StatType = statType;
            ScoreReward = scoreReward;

            switch (statType)
            {
                case StatTypes.Score:
                    Name = "Score";
                    break;

                case StatTypes.Hits:
                    Name = "Hits";
                    break;

                case StatTypes.Damage:
                    Name = "Damage";
                    break;

                case StatTypes.MostDamage:
                    Name = "Most Damage";
                    break;

                case StatTypes.Crits:
                    Name = "Crits";
                    break;

                case StatTypes.Kills:
                    Name = "Kills";
                    break;

                case StatTypes.DamageTaken:
                    Name = "Damage Taken";
                    break;

                case StatTypes.MostDamageTaken:
                    Name = "Most Damage Taken";
                    break;

                case StatTypes.HitsTaken:
                    Name = "Hits taken";
                    break;

                case StatTypes.ConsecutiveCrits:
                    Name = "Consecutive Crits";
                    break;

                case StatTypes.CritsTaken:
                    Name = "Crits Taken";
                    break;

                case StatTypes.Deaths:
                    Name = "Deaths";
                    break;

                case StatTypes.MoneySpentOnce:
                    Name = "Biggest Purchase";
                    break;

                case StatTypes.MoneyGainedOnce:
                    Name = "Biggest Earning";
                    break;

                case StatTypes.MoneySpent:
                    Name = "Money Spent";
                    break;

                case StatTypes.MoneyGained:
                    Name = "Money Earned";
                    break;

                case StatTypes.HealthGained:
                    Name = "Health Recovered";
                    break;

                case StatTypes.HealthGainedOnce:
                    Name = "Biggest Health Recovery";
                    break;
            }
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
