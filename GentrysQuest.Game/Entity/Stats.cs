namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// Stat management class
    /// </summary>
    public class Stats
    {
        public readonly IntStat Health = new IntStat("Health", StatType.Health, 500);
        public readonly IntStat Attack = new IntStat("Attack", StatType.Attack, 30);
        public readonly IntStat Defense = new IntStat("Defense", StatType.Defense, 30);
        public readonly Stat CritRate = new Stat("CritRate", StatType.CritRate, 1);
        public readonly Stat CritDamage = new Stat("CritDamage", StatType.CritDamage, 100);
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
