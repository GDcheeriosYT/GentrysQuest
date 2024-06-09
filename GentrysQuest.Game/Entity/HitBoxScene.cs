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
                if (theHitBox.CheckCollision(hitBox)) hitboxList.Add(hitBox);
            }

            return hitboxList;
        }

        public static List<CollisonHitBox> GetCollisions(CollisonHitBox theHitBox)
        {
            List<CollisonHitBox> colliders = new();

            foreach (HitBox hitBox in hitBoxes)
            {
                if (hitBox.GetType() != typeof(CollisonHitBox)) continue;

                if (theHitBox.CheckCollision(hitBox)) colliders.Add((CollisonHitBox)hitBox);
            }

            return colliders;
        }

        public static bool Collides(CollisonHitBox theHitBox)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                if (hitBox.GetType() != typeof(CollisonHitBox)) continue;

                if (theHitBox.CheckCollision(hitBox)) return true;
            }

            return false;
        }
    }
}
