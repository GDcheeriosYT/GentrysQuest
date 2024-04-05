using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        public string type; // The weapon type
        public IntStat Damage = new("Damage", StatType.Attack, 0); // Base damage
        public int AttackAmount; // How many times you've attacked
        public bool CanAttack; // If the weapon is able to attack in the current moment
        public AttackPattern AttackPattern = new(); // Defines how the weapon attacks work
        public Entity Holder; // The holder of the weapon
        public Anchor Origin = Anchor.Centre; // Design purposes
    }
}
