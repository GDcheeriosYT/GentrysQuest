using JetBrains.Annotations;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // info
        public bool isDead;

        // stats
        public Stats Stats = new Stats();

        // experience
        public Experience Experience;
        protected int difficulty;

        // equips
        [CanBeNull]
        public Weapon.Weapon Weapon;

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
            isDead = false;
            UpdateStats();
            Stats.Restore();
            OnSpawn?.Invoke();
        }

        public void Die()
        {
            isDead = true;
            OnDeath?.Invoke();
        }

        public void Damage(int amount)
        {
            Stats.Health.UpdateCurrentValue(-amount);
            if (Stats.Health.CurrentValue <= 0) Die();
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

            value += Experience.Level.current * 5;
            value += Stats.GetPointTotal() * 2;

            return value;
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

            Stats.Attack.SetDefaultValue(
                calculatePointBenefit(difficulty * 8, Stats.Attack.point, 5) +
                calculatePointBenefit(level * 2, Stats.Attack.point, 4) +
                calculatePointBenefit(starRating * 5, Stats.Attack.point, 3)
            );

            Stats.Defense.SetDefaultValue(
                calculatePointBenefit(difficulty * 10, Stats.Defense.point, 4) +
                calculatePointBenefit(level * 2, Stats.Defense.point, 2) +
                calculatePointBenefit(starRating * 3, Stats.Defense.point, 3)
            );

            Stats.CritRate.SetDefaultValue(
                calculatePointBenefit(5, Stats.CritRate.point, 5) +
                difficulty + 1
            );

            Stats.CritDamage.SetDefaultValue(
                calculatePointBenefit(difficulty * 5, Stats.CritDamage.point, 1) +
                calculatePointBenefit(starRating * 1, Stats.CritDamage.point, 1)
            );

            Stats.Speed.SetDefaultValue(
                calculatePointBenefit(0, Stats.Speed.point, 0.2)
            );

            Stats.AttackSpeed.SetDefaultValue(
                calculatePointBenefit(0, Stats.AttackSpeed.point, 0.3)
            );

            Logger.Log(Stats.ToString());

            OnUpdateStats?.Invoke();
        }

        #endregion

        private double calculatePointBenefit(double normalValue, int point, double pointBenefit)
        {
            return normalValue + (point * pointBenefit);
        }
    }
}
