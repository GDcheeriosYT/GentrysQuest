namespace GentrysQuest.Game.Database
{
    public interface IStatistic
    {
        /// <summary>
        /// The stat value
        /// </summary>
        int Value { get; }

        /// <summary>
        /// If the value should only count the best
        /// </summary>
        public bool IsConsecutive { get; }

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