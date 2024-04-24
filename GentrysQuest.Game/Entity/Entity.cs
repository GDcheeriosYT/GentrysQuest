using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // info
        public bool IsDead;

        // stats
        public Stats Stats = new();

        // experience
        public Experience Experience = new();
        protected int difficulty;

        // equips
        [CanBeNull]
        public Weapon.Weapon Weapon;

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

        // Equipment events
        public event EntityEvent OnSwapWeapon;
        public event EntityEvent OnSwapArtifact;

        // Experience events
        public event EntityEvent OnGainXp;
        public event EntityEvent OnLevelUp;

        // Other Events
        public event EntityEvent OnAttack;
        public event EntityEvent OnUpdateStats;

        #endregion

        #region Methods

        public void Spawn()
        {
            IsDead = false;
            UpdateStats();
            Stats.Restore();
            OnSpawn?.Invoke();
        }

        public void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }

        public void Attack()
        {
            OnAttack?.Invoke();
        }

        public void Damage(int amount)
        {
            Stats.Health.UpdateCurrentValue(-amount);
            if (Stats.Health.Current.Value <= 0) Die();
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
            if (Stats.Health.Current.Value <= 0) Die();
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
            Experience.Xp.CalculateRequirment(Experience.Level.Current.Value, StarRating.Value);

            UpdateStats();
            Stats.Restore();

            OnLevelUp?.Invoke();
        }

        public void SetWeapon(Weapon.Weapon weapon)
        {
            Weapon = weapon;
            weapon.Holder = this;
            OnSwapWeapon?.Invoke();
        }

        public int GetXpReward()
        {
            int value = 0;

            value += Experience.Level.Current.Value * 5;
            value += Stats.GetPointTotal() * 2;
            value += (int)(Weapon.Damage.Current.Value / 4);

            return value;
        }

        public virtual void UpdateStats()
        {
            OnUpdateStats?.Invoke();
        }

        #endregion

        protected static double CalculatePointBenefit(double normalValue, int point, double pointBenefit)
        {
            return normalValue + (point * pointBenefit);
        }
    }
}
