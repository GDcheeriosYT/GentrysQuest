using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity
{
    public interface IEffect
    {
        /// <summary>
        /// The name of the Effect duh
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The icon for this Effect
        /// </summary>
        SpriteIcon Icon { get; }

        /// <summary>
        /// Who this Effect is effecting
        /// </summary>
        Entity Effector { get; }

        bool StackAffectsTime { get; }
        bool StackAffectsEffect { get; }

        /// <summary>
        /// How this Effect will affect!
        /// </summary>
        void Handle();
    }
}
