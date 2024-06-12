namespace GentrysQuest.Game.Entity
{
    public abstract class SetBuff
    {
        /// <summary>
        /// How it applies to the character
        /// </summary>
        /// <param name="character">the character</param>
        public abstract void ApplyToCharacter(Character character);
    }
}
