namespace GentrysQuest.Game.Database
{
    public class MaxStatistic : Statistic
    {
        public MaxStatistic()
            : base()
        {
            IsConsecutive = true;
        }

        public void Set(int amount) => Value = amount;
    }
}
