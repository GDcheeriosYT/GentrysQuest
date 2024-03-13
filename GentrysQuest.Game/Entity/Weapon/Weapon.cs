using GentrysQuest.Game.Graphics;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        private string type; // The weapon type
        private int damage; // Base damage
        private int attackAmount; // How many times you've attacked
        private AttackPattern attackPattern; // Defines how the weapon attacks work
        public readonly TextureMapping TextureMapping;

        public void Attack()
        {
            // attackPattern.doSomething(theAmount, currTime);
        }
    }
}
