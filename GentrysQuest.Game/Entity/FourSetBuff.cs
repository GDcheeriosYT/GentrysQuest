namespace GentrysQuest.Game.Entity
{
    public abstract class FourSetBuff : SetBuff
    {
        public abstract override void ApplyToCharacter(Character character);

        /// <summary>
        /// What happens when the buff is removed from the character.
        /// Only requires one in this because the two set buff is just a stat boost.
        /// </summary>
        /// <param name="character"></param>
        public abstract void RemoveFromCharacter(Character character);
    }
}
