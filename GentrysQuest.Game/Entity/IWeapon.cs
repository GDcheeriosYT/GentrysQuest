namespace GentrysQuest.Game.Entity
{
    public interface IWeapon
    {
        /// <summary>
        /// The weapon type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// How many times you've attacked
        /// </summary>
        int AttackAmount { get; set; }
    }
}
