using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity.Weapon
{
    /// <summary>
    /// The effect that may apply on hit
    /// </summary>
    /// <param name="chance">represents the chance of succession. 1.0 = 100%</param>
    public class OnHitEffect(float chance)
    {
        /// <summary>
        /// The effect
        /// </summary>
        public IEffect Effect;

        /// <summary>
        /// If the effects will apply
        /// </summary>
        /// <returns>boolean</returns>
        public bool Applies() => MathBase.IsChanceSuccessful(chance);
    }
}
