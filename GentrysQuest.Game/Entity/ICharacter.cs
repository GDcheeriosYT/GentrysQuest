namespace GentrysQuest.Game.Entity
{
    public interface ICharacter
    {
        /// <summary>
        /// The artifacts of the character
        /// </summary>
        ArtifactManager Artifacts { get; }
    }
}
