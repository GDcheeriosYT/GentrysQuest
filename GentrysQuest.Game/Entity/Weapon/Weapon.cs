using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        private string type; // The weapon type
        private int damage; // Base damage
        public int AttackAmount; // How many times you've attacked
        public bool CanAttack; // If the weapon is able to attack in the current moment
        public AttackPattern AttackPattern = new(); // Defines how the weapon attacks work
        public Entity Holder;
        public Anchor origin = Anchor.Centre;
    }
}
