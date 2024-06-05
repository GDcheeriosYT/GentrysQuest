using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public class Bleed(int duration = 1, int stack = 1) : StatusEffect(duration, stack)
    {
        public override string Name { get; set; } = "Bleed";

        public override string Description { get; set; } =
            "Lose 3 + 2 per stack health every 0.5 seconds";

        public override Colour4 EffectColor { get; protected set; } = Colour4.DarkRed;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Splotch;
        public override bool IsInfinite { get; set; } = false;
        public override double Interval { get; protected set; } = new Second(0.5);

        public override void Handle()
        {
            if (ElapsedTime() > Interval * CurrentStep)
            {
                CurrentStep++;
                Effector.Damage(3 + (2 * Stack));
            }
        }
    }
}
