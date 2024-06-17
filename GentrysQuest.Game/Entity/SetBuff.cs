namespace GentrysQuest.Game.Entity
{
    public abstract class SetBuff
    {
        /// <summary>
        /// What happens when the buff is applied to the character
        /// </summary>
        /// <param name="character">the character</param>
        public abstract void ApplyToCharacter(Character character);
    }
}
