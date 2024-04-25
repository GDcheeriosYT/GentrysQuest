using System;
using System.Collections.Generic;

namespace GentrysQuest.Game.Entity
{
    public class Family
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        protected List<Artifact> Artifacts = new();
        public TwoSetBuff TwoSetBuff { get; protected set; }
        public FourSetBuff FourSetBuff { get; protected set; }

        public void AddArtifact(Artifact artifact)
        {
            Artifacts.Add(artifact);
            artifact.SetFamily(this);
        }

        public Artifact GetArtifact()
        {
            return Artifacts[Random.Shared.Next(Artifacts.Count + 1)];
        }
    }
}
