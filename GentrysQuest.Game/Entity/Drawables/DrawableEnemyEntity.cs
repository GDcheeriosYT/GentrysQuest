using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity : DrawableEntity
    {
        private DrawableEntity followEntity;

        public DrawableEnemyEntity(Enemy entity)
            : base(entity, true)
        {
            //empty constructor lol
        }

        public void FollowEntity(DrawableEntity drawableEntity)
        {
            followEntity = drawableEntity;
        }

        protected override void Update()
        {
            var value = Clock.ElapsedFrameTime * GetSpeed();
            this.MoveTo(Position + MathBase.GetDirection(Position, followEntity.Position) * (float)value);

            base.Update();
        }
    }
}
