namespace GentrysQuest.Game.Entity
{
    public interface IArtifact
    {
        /// <summary>
        /// The main attribute of this artifact.
        /// </summary>
        Buff MainAttrbitue { get; }

        /// <summary>
        /// The other attributes for this artifact.
        /// </summary>
        Buff[] Attributes { get; }
    }
}
