namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        private string type; // The weapon type
        private int damage; // Base damage
        public int AttackAmount; // How many times you've attacked
        public AttackPattern AttackPattern = new(); // Defines how the weapon attacks work

        public void Attack()
        {
            // attackPattern.doSomething(theAmount, currTime);
        }
    }
}
