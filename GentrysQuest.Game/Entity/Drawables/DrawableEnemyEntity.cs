using System;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEnemyEntity(Enemy entity) : DrawableEntity(entity, AffiliationType.Enemy, true)
    {
        private DrawableEntity followEntity;

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
