namespace GentrysQuest.Game.Entity
{
    public class Stats
    {
        private IntStat health = new IntStat("Health", StatTypes.Health, 1000);
        private IntStat attack = new IntStat("Attack", StatTypes.Attack, 30);
        private IntStat defense = new IntStat("Defense", StatTypes.Defense, 30);
        private Stat critRate = new Stat("CritRate", StatTypes.CritRate, 1);
        private Stat critDamage = new Stat("CritDamage", StatTypes.CritDamage, 100);
        private Stat speed = new Stat("Speed", StatTypes.Speed, 10);
        private Stat attackSpeed = new Stat("AttackSpeed", StatTypes.AttackSpeed, 1);

        public IntStat Health => health;

        public IntStat Attack => attack;

        public IntStat Defense => defense;

        public Stat CritRate => critRate;

        public Stat CritDamage => critDamage;

        public Stat Speed => speed;

        public Stat AttackSpeed => attackSpeed;
    }
}
