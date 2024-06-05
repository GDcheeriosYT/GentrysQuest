using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public class Paused : StatusEffect
    {
        public Paused(int duration = 1, int stack = 1)
            : base(duration, stack)
        {
            OnRemove += delegate
            {
                Effector.CanAttack = true;
                Effector.CanMove = true;
                Effector.CanDodge = true;
                Effector.HealingModifier = 1;
                Effector.DamageModifier = 1;
            };
        }

        public override string Name { get; set; } = "Paused";
        public override string Description { get; set; } = "Can't do anything";
        public override Colour4 EffectColor { get; protected set; } = Colour4.Gray;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Pause;
        public override bool IsInfinite { get; set; } = true;

        public override void Handle()
        {
            Effector.CanAttack = false;
            Effector.CanMove = false;
            Effector.CanDodge = false;
            Effector.HealingModifier = 0;
            Effector.DamageModifier = 0;
        }
    }
}
