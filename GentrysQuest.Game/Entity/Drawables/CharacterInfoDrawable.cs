using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class CharacterInfoDrawable : EntityInfoDrawable
    {
        public FillFlowContainer ArtifactContainer { get; private set; }

        public CharacterInfoDrawable(Character entity)
            : base(entity)
        {
            AddInternal(ArtifactContainer = new FillFlowContainer
            {
                Direction = FillDirection.Horizontal,
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                Margin = new MarginPadding { Right = 30 }
            });

            foreach (Artifact artifact in entity.Artifacts.Get())
            {
                ArtifactIcon anIcon = new ArtifactIcon(artifact);
                ArtifactContainer.Add(anIcon);
            }
        }
    }
}
