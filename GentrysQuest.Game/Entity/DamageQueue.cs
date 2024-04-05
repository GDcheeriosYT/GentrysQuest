using System.Collections.Generic;

namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// Used to keep track of damage so you don't repeatedly strike enemies in the frame.
    /// </summary>
    public class DamageQueue
    {
        private readonly List<HitBox> hitBoxes = new();

        public void Add(HitBox hitBox) => hitBoxes.Add(hitBox);
        public void Remove(HitBox hitBox) => hitBoxes.Remove(hitBox);
        public bool Check(HitBox hitBox) => hitBoxes.Contains(hitBox);
        public void Clear() => hitBoxes.Clear();
    }
}
