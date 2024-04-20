using System;
using GentrysQuest.Game.Utils;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity : DrawableEntity
    {
        private DrawableEntity followEntity;

        public DrawableEnemyEntity(Enemy entity)
            : base(entity, AffiliationType.Enemy, true)
        {
            OnMove += delegate(float direction, double speed)
            {
                Position += (MathBase.GetAngleToVector(direction) * SLOWING_FACTOR) * (float)(Clock.ElapsedFrameTime * GetSpeed());
            };
        }

        public void FollowEntity(DrawableEntity drawableEntity)
        {
            followEntity = drawableEntity;
        }

        protected override void Update()
        {
            Vector2 positionTo = MathBase.GetDirection(Position, followEntity.Position);

            try
            {
                Move(MathBase.GetAngle(Position, positionTo), GetSpeed());
            }
            catch (ArgumentException)
            {
                // bad coding
            }

            if (GetEntityObject().Weapon != null)
            {
                Attack(followEntity.Position);
            }

            base.Update();
        }
    }
}
