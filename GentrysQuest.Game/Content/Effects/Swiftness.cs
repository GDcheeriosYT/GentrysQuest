using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public class Swiftness : StatusEffect
    {
        public Swiftness(int duration = 1, int stack = 1)
            : base(duration, stack)
        {
            OnRemove += delegate { Effector.SpeedModifier = 1; };
        }

        public override string Name { get; set; } = "Swiftness";

        public override string Description { get; set; } =
            "Speed up for 50% + 50 per stack speed";

        public override Colour4 EffectColor { get; protected set; } = Colour4.Gray;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Wind;
        public override bool IsInfinite { get; set; } = false;

        public override void Handle()
        {
            Effector.SpeedModifier = 1 + 0.5f * Stack;
        }
    }
}
