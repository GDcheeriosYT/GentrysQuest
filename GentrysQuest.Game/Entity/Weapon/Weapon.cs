﻿using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        public string Type { get; }
        public int AttackAmount { get; set; }
        public int Distance { get; set; } // Enemy purposes
        public Stat Damage = new("Damage", StatType.Attack, 0); // Base damage
        public bool CanAttack; // If the weapon is able to attack in the current moment
        public AttackPattern AttackPattern = new(); // Defines how the weapon attacks work
        public Entity Holder; // The holder of the weapon
        public Buff Buff; // The weapon buff
        public Anchor Origin = Anchor.Centre; // Design purposes

        public delegate void HitEvent(DamageDetails details);

        public event HitEvent OnHitEntity;

        public Weapon()
        {
            Buff = new Buff(this);
            OnLevelUp += delegate
            {
                Damage.SetAdditional(Experience.Level.Current.Value * (Difficulty + 1) * StarRating.Value);
                Buff.Improve();
                Holder?.UpdateStats();
            };
        }

        public void HitEntity(DamageDetails details)
        {
            OnHitEntity?.Invoke(details);
            Holder.HitEntity(details);
        }
    }
}
