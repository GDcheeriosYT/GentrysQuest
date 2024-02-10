namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // stats
        public Stats Stats = new Stats();

        // experience
        public Experience Experience { get; } = new Experience(new Xp(0), new Level(1, 0));
        protected int difficulty;

        // equips
        protected Weapon weapon;

        #region Events

        public delegate void EntityEvent();

        // Spawn / Death events
        public event EntityEvent OnSpawn;
        public event EntityEvent OnDeath;

        // Health events
        public event EntityEvent OnDamage;
        public event EntityEvent OnHeal;

        // Movement events
        public event EntityEvent OnMove;
        public event EntityEvent OnMoveLeft;
        public event EntityEvent OnMoveRight;
        public event EntityEvent OnMoveUp;
        public event EntityEvent OnMoveDown;

        // Experience events
        public event EntityEvent OnGainXp;
        public event EntityEvent OnLevelUp;

        #endregion

        #region Methods

        public void Spawn() { OnSpawn?.Invoke(); }
        public void Die() { OnDeath?.Invoke(); }

        public void Damage(int amount)
        {
            OnDamage?.Invoke();
            Stats.Health.UpdateCurrentValue(-amount);
        }

        public void Heal(int amount)
        {
            OnHeal?.Invoke();
            Stats.Health.UpdateCurrentValue(amount);
        }

        public void AddXp(int amount)
        {
            OnGainXp?.Invoke();
            while (Experience.xp.add_xp(amount)) levelUp();
        }

        private void levelUp()
        {
            OnLevelUp?.Invoke();
            Experience.level.AddLevel();
        }

        #endregion

        public virtual void UpdateStats()
        {
            difficulty = 1 + (Experience.level.current / 20);
            Stats.Health.SetDefaultValue(Experience.level.current * 10);
            Stats.Attack.SetDefaultValue(Experience.level.current * 1.2);
            Stats.Defense.SetDefaultValue(Experience.level.current * 1.2);
            Stats.CritRate.SetDefaultValue(Experience.level.current * 0.2);
            Stats.CritDamage.SetDefaultValue(Experience.level.current * 10);
        }
    }
}
