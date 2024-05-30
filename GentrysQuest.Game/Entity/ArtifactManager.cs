using System;
using System.Linq;
using GentrysQuest.Game.Database;

namespace GentrysQuest.Game.Entity
{
    public class ArtifactManager(Character parent)
    {
        private Artifact[] artifacts = new Artifact[5];
        private float averageRating;
        public Action OnChangeArtifact;

        private void findAverageRating()
        {
            int count = artifacts.Where(artifact => artifact != null).Sum(artifact => artifact.StarRating.Value);
            averageRating = count / GetArtifactCount();
        }

        public int GetArtifactCount() => artifacts.Count(artifact => artifact != null);

        public Artifact Get(int index) => artifacts[index];

        public Artifact[] Get() => artifacts;

        public void Equip(Artifact artifact, int index)
        {
            artifacts[index] = artifact;
            artifact.Holder = parent;
            findAverageRating();
            OnChangeArtifact?.Invoke();
        }

        public void Clear() => artifacts = new Artifact[5];

        public void Remove(int index)
        {
            GameData.Artifacts.Add(artifacts[index]);
            artifacts[index].Holder = null;
            artifacts[index] = null;
            OnChangeArtifact?.Invoke();
        }
    }
}
