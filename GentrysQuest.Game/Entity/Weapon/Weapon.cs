using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public abstract class Weapon : Item
    {
        /// <summary>
        /// The weapon type
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Counter for weapon attack
        /// </summary>
        public int AttackAmount { get; set; }

        /// <summary>
        /// Describes how far away an enemy who is using the weapon needs to be to start attack an opponent
        /// </summary>
        public abstract int Distance { get; set; }

        /// <summary>
        /// A stat representing the damage.
        /// Makes more sense to have set in constructor
        /// </summary>
        public Stat Damage = new("Damage", StatType.Attack, 0); // Base damage

        /// <summary>
        /// If the weapon can attack
        /// </summary>
        public bool CanAttack;

        /// <summary>
        /// If the weapon itself can deal damage or just other things
        /// </summary>
        public virtual bool IsGeneralDamageMode { get; protected set; } = true;

        /// <summary>
        /// The attack pattern.
        /// Defines how the weapon works.
        /// Must be defined in the constructor
        /// </summary>
        public AttackPattern AttackPattern = new();

        /// <summary>
        /// Who is holding the weapon
        /// </summary>
        public Entity Holder;

        /// <summary>
        /// The weapon buff stat
        /// </summary>
        public Buff Buff;

        /// <summary>
        /// Where the weapon should be held from
        /// design purpose
        /// </summary>
        public Anchor Origin = Anchor.Centre;

        public delegate void HitEvent(DamageDetails details);

        public event HitEvent OnHitEntity;

        protected Weapon()
        {
            Buff = new Buff(this);
            OnLevelUp += delegate
            {
                UpdateStats();
                Buff.Improve();
                Holder?.UpdateStats();
            };
        }

        public void UpdateStats() => Damage.SetAdditional((Experience.Level.Current.Value - 1) * (Difficulty + 1) * StarRating.Value);

        public void HitEntity(DamageDetails details)
        {
            OnHitEntity?.Invoke(details);
            Holder.HitEntity(details);
        }
    }
}
