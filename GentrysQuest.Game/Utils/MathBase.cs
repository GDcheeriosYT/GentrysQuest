using System;
using osuTK;

namespace GentrysQuest.Game.Utils
{
    public static class MathBase
    {
        /// <summary>
        /// Get the (Normalized) direction from one vector to the other.
        /// </summary>
        /// <param name="vectorFrom">The main vector</param>
        /// <param name="vectorTo">The target vector</param>
        /// <returns>The direction from the main to the target</returns>
        public static Vector2 GetDirection(Vector2 vectorFrom, Vector2 vectorTo)
        {
            Vector2 direction = vectorTo - vectorFrom;
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Get the degrees from an angle from one vector to the other.
        /// </summary>
        /// <param name="vector1">Main vector</param>
        /// <param name="vector2">Target vector</param>
        /// <returns>The angle degrees.</returns>
        public static float GetAngle(Vector2 vector1, Vector2 vector2)
        {
            Vector2 direction = GetDirection(vector1, vector2);
            double angleRadians = Math.Atan2(direction.Y, direction.X);
            return (float)Math.Round(GetDegress(angleRadians));
        }

        /// <summary>
        /// Gets the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector to perform the math on</param>
        /// <returns>The magnitude</returns>
        public static double GetMagnitude(Vector2 vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        /// <summary>
        /// Gets the dot product of two vectors.
        /// </summary>
        /// <returns>The dot product</returns>
        public static double GetDot(Vector2 vector1, Vector2 vector2)
        {
            return (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        /// <summary>
        /// Converts radians into degrees.
        /// </summary>
        /// <param name="value">Radian value</param>
        /// <returns>The degrees</returns>
        public static double GetDegress(double value)
        {
            return value * (180 / Math.PI);
        }
    }
}
