using System;
using System.Collections.Generic;
using System.Linq;

namespace GentrysQuest.Game.Database
{
    public class StatTracker
    {
        private readonly List<IStatistic> stats = new();

        public StatTracker()
        {
            stats.Add(new Statistic(StatTypes.Score));
            stats.Add(new Statistic(StatTypes.Hits, 10));
            stats.Add(new Statistic(StatTypes.Damage));
            stats.Add(new MaxStatistic(StatTypes.MostDamage));
            stats.Add(new Statistic(StatTypes.Crits, 50));
            stats.Add(new Statistic(StatTypes.Kills, 100));
            stats.Add(new MaxStatistic(StatTypes.DamageTaken));
            stats.Add(new MaxStatistic(StatTypes.MostDamageTaken));
            stats.Add(new Statistic(StatTypes.HitsTaken, 2));
            stats.Add(new MaxStatistic(StatTypes.ConsecutiveCrits));
            stats.Add(new Statistic(StatTypes.CritsTaken));
            stats.Add(new Statistic(StatTypes.Deaths));
            stats.Add(new MaxStatistic(StatTypes.MoneySpentOnce));
            stats.Add(new MaxStatistic(StatTypes.MoneyGainedOnce));
            stats.Add(new Statistic(StatTypes.MoneySpent, 1));
            stats.Add(new Statistic(StatTypes.MoneyGained));
            stats.Add(new Statistic(StatTypes.HealthGained));
            stats.Add(new MaxStatistic(StatTypes.HealthGainedOnce));
        }

        public StatTracker(List<IStatistic> stats) => this.stats = stats;
        public IStatistic GetStat(int index) => stats[index];
        public IStatistic GetStat(string name) => stats.FirstOrDefault(t => t.Name == name);
        public IStatistic GetStat(StatTypes type) => stats.FirstOrDefault(t => t.StatType == type);

        /// <summary>
        /// Merge two StatTrackers together to combine values
        /// </summary>
        /// <param name="otherStats">The other stat tracker</param>
        public void Merge(StatTracker otherStats)
        {
            for (int statIndex = 0; statIndex < stats.Count; statIndex++)
            {
                stats[statIndex].Result(otherStats.GetStat(statIndex));
            }
        }

        /// <summary>
        /// Get the best values from two StatTrackers
        /// </summary>
        /// <param name="otherStats">Other stat tracker</param>
        /// <returns>A new StatTracker with the best values for each stat</returns>
        public StatTracker GetBest(StatTracker otherStats)
        {
            List<IStatistic> newStats = new();

            for (int statIndex = 0; statIndex < stats.Count; statIndex++)
            {
                newStats.Add(compare(stats[statIndex], otherStats.GetStat(statIndex)));
            }

            return new StatTracker(newStats);
        }

        private static IStatistic compare(IStatistic stat1, IStatistic stat2)
        {
            return (stat1.Value > stat2.Value) ? stat1 : stat2;
        }

        /// <summary>
        /// Logs stat summary to the console
        /// </summary>
        public void Log()
        {
            for (int statIndex = 0; statIndex < stats.Count; statIndex++)
            {
                Console.WriteLine(stats[statIndex].Summary());
            }
        }
    }
}
