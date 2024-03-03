using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // stats
        public Stats Stats = new Stats();

        // experience
        public Experience Experience;
        protected int difficulty;

        // equips
        protected Weapon.Weapon weapon;

        public Entity()
        {
            Experience = new Experience(new Xp(0), new Level(1));
            Experience.Xp.CalculateRequirment(1, StarRating.Value);
            UpdateStats();
        }

        #region Events

        public delegate void EntityEvent();

        public delegate void EntitySpawnEvent();

        public delegate void EntityHealthEvent(int amount);

        // Spawn / Death events
        public event EntitySpawnEvent OnSpawn;
        public event EntitySpawnEvent OnDeath;

        // Health events
        public event EntityEvent OnHealthEvent;
        public event EntityHealthEvent OnDamage;
        public event EntityHealthEvent OnHeal;
        public event EntityHealthEvent OnCrit;

        // Movement events
        public event EntityEvent OnMove;
        public event EntityEvent OnMoveLeft;
        public event EntityEvent OnMoveRight;
        public event EntityEvent OnMoveUp;
        public event EntityEvent OnMoveDown;

        // Experience events
        public event EntityEvent OnGainXp;
        public event EntityEvent OnLevelUp;

        // Other Events
        public event EntityEvent OnAttack;

        #endregion

        #region Methods

        public void Spawn() { OnSpawn?.Invoke(); }
        public void Die() { OnDeath?.Invoke(); }

        public void Damage(int amount)
        {
            Stats.Health.UpdateCurrentValue(-amount);
            OnHealthEvent?.Invoke();
            OnDamage?.Invoke(amount);
        }

        public void Heal(int amount)
        {
            Stats.Health.UpdateCurrentValue(amount);
            OnHealthEvent?.Invoke();
            OnHeal?.Invoke(amount);
        }

        public void Crit(int amount)
        {
            Stats.Health.UpdateCurrentValue(-amount);
            OnHealthEvent?.Invoke();
            OnCrit?.Invoke(amount);
        }

        public void AddXp(int amount)
        {
            while (Experience.Xp.add_xp(amount)) LevelUp();
            OnGainXp?.Invoke();
        }

        public void LevelUp()
        {
            Experience.Level.AddLevel();
            Experience.Xp.CalculateRequirment(Experience.Level.current, StarRating.Value);

            UpdateStats();

            OnLevelUp?.Invoke();
        }

        public void UpdateStats()
        {
            int level = Experience.Level.current;
            int starRating = StarRating.Value;
            difficulty = 1 + level / 20;

            Stats.Health.SetDefaultValue(
                calculatePointBenefit(difficulty * 100, Stats.Health.point, 500) +
                calculatePointBenefit(level * 50, Stats.Health.point, 10) +
                calculatePointBenefit(starRating * 50, Stats.Health.point, 50)
            );
            Stats.Attack.SetDefaultValue(Experience.Level.current * 1.2);
            Stats.Defense.SetDefaultValue(Experience.Level.current * 1.2);
            Stats.CritRate.SetDefaultValue(Experience.Level.current * 0.2);
            Stats.CritDamage.SetDefaultValue(Experience.Level.current * 10);

            Logger.Log(Stats.ToString());
        }

        #endregion

        private int calculatePointBenefit(int normalValue, int point, int pointBenefit)
        {
            return normalValue + (point * pointBenefit);
        }
    }
}
