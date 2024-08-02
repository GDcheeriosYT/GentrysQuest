using System.Collections.Generic;
using GentrysQuest.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity : DrawableEntity
    {
        private DrawableEntity followEntity;
        private EnemyController enemyController;

        public DrawableEnemyEntity(Entity entity)
            : base(entity, AffiliationType.Enemy)
        {
            OnMove += delegate(Vector2 direction, double speed)
            {
                Position += direction * (float)Clock.ElapsedFrameTime * (float)speed;
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(enemyController = new EnemyController(this));
        }

        public void FollowEntity(DrawableEntity drawableEntity) => followEntity = drawableEntity;

        private Vector2 GetDesirableDirection()
        {
            float score = 0f;

            foreach (KeyValuePair<int, bool> angle in enemyController.GetIntersectedAngles())
            {
                Vector2 toPoint = (MathBase.GetAngleToVector((float)angle.Key) - Position).Normalized();
                float result = Vector2.Dot(getDirectionToPlayer(), toPoint);
                score += angle.Value ? result : -result;
            }

            Logger.Log(MathBase.GetAngleToVector(score).ToString());
            return MathBase.GetAngleToVector(score);
        }

        private Vector2 getDirectionToPlayer() => MathBase.GetDirection(Position, followEntity.Position);

        protected override void Update()
        {
            base.Update();

            if (followEntity == null) return;

            if (GetBase().Weapon != null && MathBase.GetDistance(Position, followEntity.Position) < GetBase().Weapon!.Distance) Attack(followEntity.Position);

            DirectionLooking = (int)MathBase.GetAngle(Position, followEntity.Position);

            if (Entity.CanMove) Direction += GetDesirableDirection();
            if (Direction != Vector2.Zero) Move(Direction.Normalized(), GetSpeed());
        }
    }
}
