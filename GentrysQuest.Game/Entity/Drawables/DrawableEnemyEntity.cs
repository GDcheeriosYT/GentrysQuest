using System;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity : DrawableEntity
    {
        private DrawableEntity followEntity;

        public DrawableEnemyEntity(Enemy entity)
            : base(entity, AffiliationType.Enemy, true)
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

            try
            {
                this.MoveTo(Position + MathBase.GetDirection(Position, followEntity.Position) * (float)value);
            }
            catch (ArgumentException)
            {
                Logger.Log("Error");
            }

            base.Update();
        }
    }
}
