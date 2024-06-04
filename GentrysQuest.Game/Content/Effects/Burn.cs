using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Content.Effects;

public class Burn : StatusEffect
{
    public override string Name { get; set; } = "Burn";

    public override string Description { get; set; } =
        "Burns enemies for 1% + 1 per stack of health every second.";

    public override Colour4 EffectColor { get; protected set; } = Colour4.Orange;

    public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Fire;

    public override void Handle()
    {
        if (ElapsedTime() > Interval * CurrentStep)
        {
            CurrentStep++;
            Effector.Damage((int)Effector.Stats.Health.GetPercentFromTotal(Stack));
        }
    }
}
