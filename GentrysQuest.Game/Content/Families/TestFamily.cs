using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families
{
    public class TestFamily : Family
    {
        public TestFamily()
        {
            Name = "Test Family";
            Description = "Just a family for testing";
            Artifacts.Add(typeof(TestArtifact));
        }
    }

    public class TestArtifact : Artifact
    {
        public TestArtifact()
            : base()
        {
            Name = "Test Artifact";
        }

        public override Family family { get; protected set; } = new TestFamily();
    }
}
