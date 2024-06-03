using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity;

public abstract class StatusEffect(int stack = 1)
{
    /// <summary>
    /// The name of the Effect duh
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    /// Description of the effect
    /// </summary>
    public abstract string Description { get; set; }

    /// <summary>
    /// The color for this Effect.
    /// </summary>
    public abstract Colour4 EffectColor { get; protected set; }

    /// <summary>
    /// the icon for this effect
    /// </summary>
    public abstract IconUsage Icon { get; protected set; }

    /// <summary>
    /// Who this Effect is effecting
    /// </summary>
    protected Entity Effector { get; private set; }

    /// <summary>
    /// when the effect started
    /// </summary>
    public double? StartTime = null;

    /// <summary>
    /// is it effecting?
    /// </summary>
    public bool Effecting = false;

    /// <summary>
    /// The time between effect
    /// </summary>
    public abstract double Interval { get; protected set; }

    /// <summary>
    /// How long the effect lasts
    /// </summary>
    public abstract int Duration { get; protected set; }

    /// <summary>
    /// How much of this effect is applied
    /// </summary>
    public int Stack = stack;

    /// <summary>
    /// How this Effect will affect!
    /// </summary>
    public abstract void Handle();
}
