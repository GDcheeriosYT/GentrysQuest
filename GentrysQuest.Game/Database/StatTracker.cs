namespace GentrysQuest.Game.Database
{
    public class StatTracker
    {
        public Statistic Score { get; private set; }
        public Statistic Hits { get; private set; }
        public Statistic Damage { get; private set; }
        public MaxStatistic MostDamage { get; private set; }
        public Statistic Crits { get; private set; }
        public Statistic Kills { get; private set; }
        public Statistic DamageTaken { get; private set; }
        public MaxStatistic MostDamageTaken { get; private set; }
        public Statistic HitsTaken { get; private set; }
        public MaxStatistic ConsecutiveCrits { get; private set; }
        public Statistic CritsTaken { get; private set; }
        public Statistic Deaths { get; private set; }
        public MaxStatistic MoneySpentOnce { get; private set; }
        public MaxStatistic MoneyGainedOnce { get; private set; }
        public Statistic MoneySpent { get; private set; }
        public Statistic MoneyGained { get; private set; }
        public Statistic HealthGained { get; private set; }
        public MaxStatistic HealthGainedOnce { get; private set; }

        public StatTracker()
        {
            Score = new();
            Hits = new();
            Damage = new();
            MostDamage = new();
            Crits = new();
            Kills = new();
            DamageTaken = new();
            MostDamageTaken = new();
            HitsTaken = new();
            ConsecutiveCrits = new();
            CritsTaken = new();
            Deaths = new();
            MoneySpentOnce = new();
            MoneyGainedOnce = new();
            MoneySpent = new();
            MoneyGained = new();
            HealthGained = new();
            HealthGainedOnce = new();
        }

        public StatTracker(
            Statistic score,
            Statistic hits,
            Statistic damage,
            MaxStatistic mostDamage,
            Statistic crits,
            Statistic kills,
            Statistic damageTaken,
            MaxStatistic mostDamageTaken,
            Statistic hitsTaken,
            MaxStatistic consecutiveCrits,
            Statistic critsTaken,
            Statistic deaths,
            MaxStatistic moneySpentOnce,
            MaxStatistic moneyGainedOnce,
            Statistic moneySpent,
            Statistic moneyGained,
            Statistic healthGained,
            MaxStatistic healthGainedOnce
        )
        {
            Score = score;
            Hits = hits;
            Damage = damage;
            MostDamage = mostDamage;
            Crits = crits;
            Kills = kills;
            DamageTaken = damageTaken;
            MostDamageTaken = mostDamageTaken;
            HitsTaken = hitsTaken;
            ConsecutiveCrits = consecutiveCrits;
            CritsTaken = critsTaken;
            Deaths = deaths;
            MoneySpentOnce = moneySpentOnce;
            MoneyGainedOnce = moneyGainedOnce;
            MoneySpent = moneySpent;
            MoneyGained = moneyGained;
            HealthGained = healthGained;
            HealthGainedOnce = healthGainedOnce;
        }

        public void Merge(StatTracker otherStats)
        {
            Score.Result(otherStats.Score);
            Hits.Result(otherStats.Hits);
            Damage.Result(otherStats.Damage);
            MostDamage.Result(otherStats.MostDamage);
            Crits.Result(otherStats.Crits);
            Kills.Result(otherStats.Kills);
            DamageTaken.Result(otherStats.DamageTaken);
            MostDamageTaken.Result(otherStats.MostDamageTaken);
            HitsTaken.Result(otherStats.HitsTaken);
            ConsecutiveCrits.Result(otherStats.ConsecutiveCrits);
            CritsTaken.Result(otherStats.CritsTaken);
            Deaths.Result(otherStats.Deaths);
            MoneySpentOnce.Result(otherStats.MoneySpentOnce);
            MoneyGainedOnce.Result(otherStats.MoneyGainedOnce);
            MoneySpent.Result(otherStats.MoneySpent);
            MoneyGained.Result(otherStats.MoneyGained);
            HealthGained.Result(otherStats.HealthGained);
            HealthGainedOnce.Result(otherStats.HealthGainedOnce);
        }

        public StatTracker GetBest(StatTracker otherStats)
        {
            return new StatTracker(
                compare(Score, otherStats.Score),
                compare(Hits, otherStats.Hits),
                compare(Damage, otherStats.Damage),
                compare(MostDamage, otherStats.MostDamage),
                compare(Crits, otherStats.Crits),
                compare(Kills, otherStats.Kills),
                compare(DamageTaken, otherStats.DamageTaken),
                compare(MostDamageTaken, otherStats.MostDamageTaken),
                compare(HitsTaken, otherStats.HitsTaken),
                compare(ConsecutiveCrits, otherStats.ConsecutiveCrits),
                compare(CritsTaken, otherStats.CritsTaken),
                compare(Deaths, otherStats.Deaths),
                compare(MoneySpentOnce, otherStats.MoneySpentOnce),
                compare(MoneyGainedOnce, otherStats.MoneyGainedOnce),
                compare(MoneySpent, otherStats.MoneySpent),
                compare(MoneyGained, otherStats.MoneyGained),
                compare(HealthGained, otherStats.HealthGained),
                compare(HealthGainedOnce, otherStats.HealthGainedOnce)
            );
        }

        private static Statistic compare(Statistic stat1, Statistic stat2)
        {
            return (stat1.Value > stat2.Value) ? stat1 : stat2;
        }

        private static MaxStatistic compare(MaxStatistic stat1, MaxStatistic stat2)
        {
            return (stat1.Value > stat2.Value) ? stat1 : stat2;
        }
    }
}
