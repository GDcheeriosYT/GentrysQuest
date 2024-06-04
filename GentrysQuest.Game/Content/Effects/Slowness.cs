using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects
{
    public sealed class Slowness : StatusEffect
    {
        public override string Name { get; set; } = "Slowness";

        public override string Description { get; set; } =
            "Slows enemies for 50% + 50 per stack of current speed.";

        public override Colour4 EffectColor { get; protected set; } = Colour4.Gray;
        public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Cloud;

        public override void Handle()
        {
            var amount = Effector.Stats.Speed.GetPercentFromTotal(50 / Stack);
            Effector.Stats.Speed.Current.Value = amount;
        }
    }
}
