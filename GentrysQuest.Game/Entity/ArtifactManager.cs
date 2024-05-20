using System;
using GentrysQuest.Game.Database;

namespace GentrysQuest.Game.Entity
{
    public class ArtifactManager(Character parent)
    {
        private Artifact[] artifacts { get; } = new Artifact[5];
        private float averageRating;
        public Action OnChangeArtifact;
        private Character parent = parent;

        private void findAverageRating()
        {
            int count = 0;

            foreach (Artifact artifact in artifacts)
            {
                count += artifact.StarRating.Value;
            }

            averageRating = count / 5f;
        }

        public Artifact Get(int index) => artifacts[index];

        public Artifact[] Get() => artifacts;

        public void Equip(Artifact artifact, int index)
        {
            artifacts[index] = artifact;
            artifact.Holder = parent;
            OnChangeArtifact?.Invoke();
        }

        public void Remove(int index)
        {
            GameData.Artifacts.Add(artifacts[index]);
            artifacts[index].Holder = null;
            artifacts[index] = null;
            OnChangeArtifact?.Invoke();
        }
    }
}
