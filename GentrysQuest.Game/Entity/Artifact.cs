namespace GentrysQuest.Game.Entity
{
    public class Artifact : EntityBase, IArtifact
    {
        public Family Family { get; private set; } = null;
        public Buff MainAttribute { get; set; }
        public Buff[] Attributes { get; set; }

        public void SetFamily(Family family)
        {
            if (Family != null) Family = family;
        }
    }
}
