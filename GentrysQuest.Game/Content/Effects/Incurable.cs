using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;

namespace GentrysQuest.Game.Content.Effects
{
    public class Incurable : StatusEffect
    {
        public Incurable(int duration = 1, int stack = 1)
            : base(duration, stack) =>
            OnRemove += delegate
            {
                Effector.HealingModifier = 1;
            };

        public override string Name { get; set; } = "Incurable";
        public override string Description { get; set; } = "Can't heal";
        public override Colour4 EffectColor { get; protected set; } = Colour4.Gold;
        public override bool IsInfinite { get; set; } = true;

        public override void Handle()
        {
            Effector.HealingModifier = 0;
        }
    }
}
