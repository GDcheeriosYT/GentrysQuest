using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families
{
    public class TestFamily : Family
    {
        public TestFamily()
        {
            Name = "Test Family";
            Description = "Just a family for testing";
            AddArtifact(new TestArtifact());
        }
    }

    public class TestArtifact : Artifact, IArtifact
    {
        public TestArtifact()
            : base()
        {
            Name = "Test Artifact";
        }
    }
}
