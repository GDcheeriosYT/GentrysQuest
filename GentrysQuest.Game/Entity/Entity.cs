using GentrysQuest.Game.Database;
using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // info
        public bool IsDead;

        // stats
        public Stats Stats = new();

        // equips
        [CanBeNull]
        public Weapon.Weapon Weapon;

        public Entity()
        {
            OnLevelUp += UpdateStats;
            OnLevelUp += Stats.Restore;
            OnSwapWeapon += UpdateStats;
        }

        #region Events

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
            GameData.currentStats.Deaths.Add();
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
            GameData.currentStats.DamageTaken.Add(amount);
            if (GameData.currentStats.MostDamageTaken.Bigger(amount)) GameData.currentStats.MostDamageTaken.Set(amount);
            GameData.currentStats.HitsTaken.Add();
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

        public void SetWeapon([CanBeNull] Weapon.Weapon weapon)
        {
            Weapon = weapon;
            if (weapon != null) weapon.Holder = this;
            OnSwapWeapon?.Invoke();
        }

        public int GetXpReward()
        {
            int value = 0;

            value += Experience.Level.Current.Value * 5;
            value += Stats.GetPointTotal() * 2;
            if (Weapon != null) value += (int)(Weapon.Damage.Current.Value / 4);

            return value;
        }

        public int GetMoneyReward()
        {
            int value = 0;

            value += Experience.Level.Current.Value;
            value += Stats.GetPointTotal();

            return value;
        }

        public virtual Weapon.Weapon GetWeaponReward()
        {
            return Weapon;
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
