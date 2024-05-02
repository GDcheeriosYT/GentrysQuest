using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class ArtifactInfoDrawable : EntityInfoDrawable
    {
        private FillFlowContainer buffsListContainer;

        public ArtifactInfoDrawable(Artifact entity)
            : base(entity)
        {
            BuffContainer.Add(new DrawableBuffIcon(entity.MainAttribute));
            AddInternal(buffsListContainer = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                AutoSizeAxes = Axes.Both,
                Padding = new MarginPadding { Right = 12 },
            });

            foreach (Buff attribute in entity.Attributes)
            {
                buffsListContainer.Add(new DrawableBuffIcon(attribute, true)
                {
                    Size = new Vector2(16)
                });
            }
        }
    }
}
