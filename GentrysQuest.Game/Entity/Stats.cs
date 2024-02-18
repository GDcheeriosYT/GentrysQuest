namespace GentrysQuest.Game.Entity
{
    public class Stats
    {
        public readonly IntStat Health = new IntStat("Health", StatType.Health, 500);
        public readonly IntStat Attack = new IntStat("Attack", StatType.Attack, 30);
        public readonly IntStat Defense = new IntStat("Defense", StatType.Defense, 30);
        public readonly Stat CritRate = new Stat("CritRate", StatType.CritRate, 1);
        public readonly Stat CritDamage = new Stat("CritDamage", StatType.CritDamage, 100);
        public readonly Stat Speed = new Stat("Speed", StatType.Speed, 1);
        public readonly Stat AttackSpeed = new Stat("AttackSpeed", StatType.AttackSpeed, 1);

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
