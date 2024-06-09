namespace GentrysQuest.Game.Database
{
    public interface IStatistic
    {
        /// <summary>
        /// Name of the stat
        /// </summary>
        string Name { get; }

        StatTypes StatType { get; }

        /// <summary>
        /// The stat value
        /// </summary>
        float Value { get; }

        /// <summary>
        /// How much score the stat rewards
        /// </summary>
        float ScoreReward { get; }

        /// <summary>
        /// If the value should only count the best
        /// </summary>
        bool IsConsecutive { get; }

        /// <summary>
        /// Controls how adding to the stat works
        /// </summary>
        void Add(float amount = 1);

        /// <summary>
        /// Returns the summary of the stat
        /// </summary>
        string Summary();

        /// <summary>
        /// sets the value based on two stats
        /// </summary>
        /// <param name="otherStat">other stat to compare</param>
        void Result(IStatistic otherStat);

        /// <summary>
        /// is number bigger?
        /// </summary>
        /// <param name="amount">number to check</param>
        /// <returns>boolean representing if bigger</returns>
        bool Bigger(int amount);
    }
}
