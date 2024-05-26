namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// Stat management class /// </summary>
    public class Stats
    {
        public readonly Stat Health = new Stat("Health", StatType.Health, 500, false);
        public readonly Stat Attack = new Stat("Attack", StatType.Attack, 10);
        public readonly Stat Defense = new Stat("Defense", StatType.Defense, 6);
        public readonly Stat CritRate = new Stat("CritRate", StatType.CritRate, 1);
        public readonly Stat CritDamage = new Stat("CritDamage", StatType.CritDamage, 20);
        public readonly Stat Speed = new Stat("Speed", StatType.Speed, 1);
        public readonly Stat AttackSpeed = new Stat("AttackSpeed", StatType.AttackSpeed, 1);
        private readonly Stat[] statGrouping;

        public Stats()
        {
            statGrouping = new Stat[]
            {
                Health,
                Attack,
                Defense,
                CritRate,
                CritDamage,
                Speed,
                AttackSpeed
            };
        }

        public Stat GetStat(string name)
        {
            foreach (Stat stat in statGrouping)
            {
                if (name == stat.Name) return stat;
            }

            return null;
        }

        public Stat[] GetStats() => statGrouping;

        /// <summary>
        /// Restores all stat values to original value
        /// </summary>
        public void Restore()
        {
            foreach (Stat stat in statGrouping)
            {
                stat.RestoreValue();
            }
        }

        /// <summary>
        /// Resets all additional values
        /// </summary>
        public void ResetAdditionalValues()
        {
            foreach (Stat stat in statGrouping)
            {
                stat.ResetAdditionalValue();
            }
        }

        public int GetPointTotal()
        {
            int points = 1;

            foreach (Stat stat in statGrouping)
            {
                points += stat.point;
            }

            return points;
        }

        public override string ToString()
        {
            return $"{Health}\n"
                   + $"{Attack}\n"
                   + $"{Defense}\n"
                   + $"{CritRate}\n"
                   + $"{CritDamage}\n"
                   + $"{Speed}\n"
                   + $"{AttackSpeed}";
        }
    }
}
