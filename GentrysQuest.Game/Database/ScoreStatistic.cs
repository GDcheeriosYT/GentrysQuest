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

        public override void Add(float amount)
        {
            base.Add(amount);
            OnScoreChange?.Invoke();
        }
    }
}
