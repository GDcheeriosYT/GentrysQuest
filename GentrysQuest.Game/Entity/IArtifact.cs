namespace GentrysQuest.Game.Entity
{
    public interface IArtifact
    {
        Family Family { get; }

        /// <summary>
        /// The main attribute of this artifact.
        /// </summary>
        Buff MainAttribute { get; protected set; }

        /// <summary>
        /// The other attributes for this artifact.
        /// </summary>
        Buff[] Attributes { get; protected set; }
    }
}
