using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Entity
{
    public partial class EnemyController : CompositeDrawable
    {
        private DrawableEntity parent;
        private CollisonHitBox[] directionChecks;
        private const int DIRECTIONS = 12;
        private const float DISTANCE = 1f;

        public EnemyController(DrawableEntity enemy)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            parent = enemy;

            directionChecks = new CollisonHitBox[DIRECTIONS];

            for (int i = 0; i < DIRECTIONS; i++)
            {
                var rotation = i * (360 / DIRECTIONS);
                directionChecks[i] = new CollisonHitBox(enemy)
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 0.1f),
                    Rotation = rotation,
                    Position = MathBase.GetAngleToVector(rotation) * DISTANCE
                };
                AddInternal(directionChecks[i]);
            }
        }
    }
}
