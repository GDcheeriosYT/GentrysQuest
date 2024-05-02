using System.Collections.Generic;

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
        List<Buff> Attributes { get; protected set; }

        List<StatType> ValidMainAttributes { get; protected set; }
        List<int> ValidStarRatings { get; protected set; }
        AllowedPercentMethod AllowedPercentMethod { get; protected set; }
    }
}
