using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public sealed class Slowness : StatusEffect
    {
        public Slowness(int duration = 1, int stack = 1)
            : base(duration, stack)
        {
            OnRemove += delegate { Effector.SpeedModifier = 1; };
        }

        public override string Name { get; set; } = "Slowness";

        public override string Description { get; set; } =
            "Slow down for 50% + 50 per stack of speed.";

        public override Colour4 EffectColor { get; protected set; } = Colour4.Gray;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Cloud;
        public override bool IsInfinite { get; set; } = false;

        public override void Handle()
        {
            Effector.SpeedModifier = 0.5f / Stack;
        }
    }
}
