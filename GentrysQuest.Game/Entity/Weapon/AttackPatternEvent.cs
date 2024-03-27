using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPatternEvent
    {
        private DrawableWeapon weaponInstance;

        public float? Direction = 0;
        public Vector2? Position = new Vector2(1);
        public Vector2? Size = new Vector2(1);
        public Vector2? HitboxSize = new Vector2(1);
        public float? Distance = 0;
        public int DamagePercent = 100;

        public void Activate(int timeMS)
        {
            if (Direction != null) weaponInstance.RotateTo((float)Direction, duration: timeMS);
            if (Position != null) weaponInstance.MoveTo((Vector2)Position, duration: timeMS);
            if (Size != null) weaponInstance.ResizeTo((Vector2)Size, duration: timeMS);
            if (HitboxSize != null) weaponInstance.MoveTo((Vector2)HitboxSize, duration: timeMS);
            // if (Distance != null) weaponInstance.MoveTo((float)Distance);
        }
    }
}
