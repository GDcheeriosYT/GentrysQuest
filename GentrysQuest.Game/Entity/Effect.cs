using System;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// Base effect class
    /// </summary>
    public class Effect : IEffect
    {
        public string Name { get; }
        public SpriteIcon Icon { get; }
        public Entity Effector { get; }
        public bool StackAffectsTime { get; }
        public bool StackAffectsEffect { get; }

        public void Handle()
        {
            throw new NotImplementedException();
        }
    }
}
