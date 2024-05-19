namespace GentrysQuest.Game.Entity
{
    public class ArtifactManager
    {
        private Artifact[] artifacts { get; } = new Artifact[5];
        private float averageRating;

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

        public void Equip(Artifact artifact, int index) => artifacts[index] = artifact;
    }
}
