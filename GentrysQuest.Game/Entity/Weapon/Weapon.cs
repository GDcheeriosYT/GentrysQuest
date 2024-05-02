using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item, IWeapon
    {
        public string Type { get; }
        public int AttackAmount { get; set; }
        public Stat Damage = new("Damage", StatType.Attack, 0); // Base damage
        public bool CanAttack; // If the weapon is able to attack in the current moment
        public AttackPattern AttackPattern = new(); // Defines how the weapon attacks work
        public Entity Holder; // The holder of the weapon
        public Buff Buff; // The weapon buff
        public Anchor Origin = Anchor.Centre; // Design purposes

        public Weapon()
        {
            Buff = new Buff(this);
        }
    }
}
