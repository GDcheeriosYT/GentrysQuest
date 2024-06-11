namespace GentrysQuest.Game.Database
{
    public class ScoreStatistic : Statistic
    {
        public delegate void ScoreEvent(int change);

        public ScoreEvent OnScoreChange;

        public ScoreStatistic()
            : base(StatTypes.Score)
        {
        }

        public override void Add(float amount)
        {
            base.Add(amount);
            OnScoreChange?.Invoke((int)amount);
        }
    }
}
