namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // stats
        public Stats stats = new Stats();

        // experience
        protected Experience experience = new Experience(new Xp(0), new Level(1, 0));
        protected int difficulty;

        // equips
        protected Weapon weapon;

        public virtual void UpdateStats()
        {
            difficulty = 1 + (experience.level.current / 20);
            stats.Health.SetDefaultValue(experience.level.current * 10);
            stats.Attack.SetDefaultValue(experience.level.current * 1.2);
            stats.Defense.SetDefaultValue(experience.level.current * 1.2);
            stats.CritRate.SetDefaultValue(experience.level.current * 0.2);
            stats.CritDamage.SetDefaultValue(experience.level.current * 10);
        }
    }
}
