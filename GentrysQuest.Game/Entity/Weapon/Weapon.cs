namespace GentrysQuest.Game.Entity.Weapon
{
    public class Weapon : Item
    {
        private string type; // The weapon type
        private int damage; // Base damage
        private int time; // The amount of time it takes for the attack pattern to finish
        private AttackPattern attackPattern; // Defines how the weapon attacks work
    }
}
