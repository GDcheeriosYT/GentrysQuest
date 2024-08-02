using GentrysQuest.Game.Utils;
using osu.Framework.Allocation;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity : DrawableEntity
    {
        private DrawableEntity followEntity;
        private EnemyController directionHandler;

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
            AddInternal(directionHandler = new EnemyController(this));
        }

        public void FollowEntity(DrawableEntity drawableEntity)
        {
            followEntity = drawableEntity;
        }

        protected override void Update()
        {
            base.Update();

            if (followEntity == null) return;

            if (GetEntityObject().Weapon != null && MathBase.GetDistance(Position, followEntity.Position) < GetEntityObject().Weapon!.Distance) Attack(followEntity.Position);

            DirectionLooking = (int)MathBase.GetAngle(Position, followEntity.Position);
            if (Entity.CanMove) Direction += MathBase.GetDirection(Position, followEntity.Position);
            if (Direction != Vector2.Zero) Move(Direction.Normalized(), GetSpeed());
        }
    }
}
