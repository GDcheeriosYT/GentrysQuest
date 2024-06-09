using System;

namespace GentrysQuest.Game.Database
{
    public class ScoreStatistic : Statistic
    {
        public Action OnScoreChange;

        public ScoreStatistic()
            : base(StatTypes.Score)
        {
        }

        public override void Add(int amount)
        {
            base.Add(amount);
            if (amount != 0) OnScoreChange?.Invoke();
        }
    }
}
