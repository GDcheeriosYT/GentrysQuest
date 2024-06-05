using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public class BraydenBoost : StatusEffect
    {
        public override string Name { get; set; } = "Brayden Boost";
        public override string Description { get; set; } = "The brayden boost";
        public override Colour4 EffectColor { get; protected set; } = Colour4.Gold;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.UserPlus;
        public override bool IsInfinite { get; set; } = true;

        private bool boosted;

        public override void Handle()
        {
            if (boosted) return;

            Effector.Stats.Boost(Stack * 20);
            boosted = true;
        }
    }
}
