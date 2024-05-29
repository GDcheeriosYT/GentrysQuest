using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPatternEvent(int timeMs)
    {
        public int TimeMs = timeMs;
        public float? Direction = 0;
        public Vector2? Position = new Vector2(1);
        public Vector2? Size = new Vector2(1);
        public Vector2? HitboxSize = new Vector2(1);
        public float? Distance = 0;
        public int DamagePercent = 0;
        public float? MovementSpeed = 1;
        public Easing Transition = Easing.None;
        public bool ResetHitBox = false;

        public AttackPatternEvent()
            : this(0)
        {
        }

        public override string ToString()
        {
            return $"{timeMs}\n"
                   + $"{Direction}\n"
                   + $"{Position}\n"
                   + $"{Size}\n"
                   + $"{HitboxSize}\n"
                   + $"{Distance}\n"
                   + $"{DamagePercent}";
        }
    }
}
