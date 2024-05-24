namespace GentrysQuest.Game.Database
{
    public class MaxStatistic : Statistic
    {
        public MaxStatistic(StatTypes statType, short scoreReward = 0)
            : base(statType, scoreReward)
        {
            IsConsecutive = true;
        }

        public void Set(int amount) => Value = amount;
    }
}
