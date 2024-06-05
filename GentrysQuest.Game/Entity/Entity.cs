﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity
{
    public class Entity : EntityBase
    {
        // info
        public bool IsDead;
        public bool IsFullHealth;

        // stats
        public Stats Stats = new();

        // equips
        [CanBeNull]
        public Weapon.Weapon Weapon;

        // effects
        public List<StatusEffect> Effects = new();

        // Stat Modifiers
        // Used for quick use cases like if dodging or blah blah blah
        public float SpeedModifier = 1;
        public bool IsDodging = false;
        public bool CanDodge = true;
        public bool CanAttack = true;
        public bool CanMove = true;

        public Entity()
        {
            OnLevelUp += UpdateStats;
            OnLevelUp += Stats.Restore;
            OnSwapWeapon += UpdateStats;
            CalculateXpRequirement();
        }

        #region Events

        public delegate void EntitySpawnEvent();

        public delegate void EntityHealthEvent(int amount);

        public delegate void EntityHitEvent(DamageDetails details);

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

        // Combat events
        public event EntityEvent OnAttack;
        public event EntityHitEvent OnHitEntity;
        public event EntityHitEvent OnGetHit;

        // Other Events
        public event EntityEvent OnUpdateStats;
        public event Action OnEffect;

        #endregion

        #region Methods

        public void Spawn()
        {
            CanMove = true;
            CanAttack = true;
            IsDead = false;
            OnSpawn?.Invoke();
        }

        public virtual void Die()
        {
            CanMove = false;
            CanAttack = false;
            IsDead = true;
            OnDeath?.Invoke();
        }

        public virtual void Attack()
        {
            OnAttack?.Invoke();
        }

        public virtual void Damage(int amount)
        {
            if (amount <= 0) amount = 1;
            if (IsDodging) amount = 0;
            Stats.Health.UpdateCurrentValue(-amount);
            if (Stats.Health.Current.Value <= 0 && !IsDead) Die();
            OnHealthEvent?.Invoke();
            OnDamage?.Invoke(amount);
        }

        public void HitEntity(DamageDetails details) => OnHitEntity?.Invoke(details);
        public void OnHit(DamageDetails details) => OnGetHit?.Invoke(details);

        public virtual void Crit(int amount)
        {
            if (amount <= 0) amount = 1;
            if (IsDodging) amount = 0;
            Damage(amount);
            OnCrit?.Invoke(amount);
        }

        public virtual void Heal(int amount)
        {
            Stats.Health.UpdateCurrentValue(amount);
            IsFullHealth = Stats.Health.Current.Value == Stats.Health.Total();
            OnHealthEvent?.Invoke();
            OnHeal?.Invoke(amount);
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

        public void AddEffect(StatusEffect statusEffect)
        {
            bool inList = false;
            statusEffect.SetEffector(this);

            foreach (var effect in Effects.Where(effect => effect.GetType() == statusEffect.GetType()))
            {
                effect.Stack++;
                inList = true;
            }

            if (!inList) Effects.Add(statusEffect);
            OnEffect?.Invoke();
        }

        public void RemoveEffect(string name)
        {
            for (var index = 0; index < Effects.Count; index++)
            {
                var effect = Effects[index];

                if (effect.Name != name) continue;

                Effects.Remove(effect);
                int health = (int)Stats.Health.Current.Value;
                UpdateStats();

                // because the stats get reset we set health to normal
                Stats.Health.Current.Value = health;
            }

            OnEffect?.Invoke();
        }

        public void Affect(double time)
        {
            foreach (StatusEffect effect in Effects.ToList())
            {
                effect.SetTime(time);
                effect.Handle();

                if (time - effect.StartTime > effect.Duration)
                {
                    if (effect.Stack == 1) RemoveEffect(effect.Name);
                    else effect.Stack--;
                }
            }

            OnEffect?.Invoke();
        }

        /// <summary>
        /// Defines how stats will update
        /// </summary>
        public virtual void UpdateStats() => OnUpdateStats?.Invoke();

        #endregion

        protected static double CalculatePointBenefit(double normalValue, int point, double pointBenefit)
        {
            return normalValue + (point * pointBenefit);
        }
    }
}
