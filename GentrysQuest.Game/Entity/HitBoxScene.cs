using System.Collections.Generic;

namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// The hitbox scene class.
    /// Manages all the hitboxes
    /// </summary>
    public static class HitBoxScene
    {
        private static List<HitBox> hitBoxes = new();

        public static void Add(HitBox hitBox) => hitBoxes.Add(hitBox);
        public static void Remove(HitBox hitBox) => hitBoxes.Remove(hitBox);

        /// <summary>
        /// Gets intersections for hitboxes.
        /// </summary>
        /// <param name="theHitBox">The hitbox to check for intersections</param>
        /// <returns></returns>
        public static List<HitBox> GetIntersections(HitBox theHitBox)
        {
            List<HitBox> hitboxList = new();

            foreach (HitBox hitBox in hitBoxes)
            {
                if (theHitBox.CheckCollision(hitBox) && hitBox.GetType() != typeof(CollisonHitBox)) hitboxList.Add(hitBox);
            }

            return hitboxList;
        }
    }
}
