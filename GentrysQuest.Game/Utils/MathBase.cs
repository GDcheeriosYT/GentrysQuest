using System;
using osuTK;

namespace GentrysQuest.Game.Utils
{
    public static class MathBase
    {
        public static Vector2 GetDirection(Vector2 vectorFrom, Vector2 vectorTo)
        {
            Vector2 direction = vectorTo - vectorFrom;
            direction.Normalize();
            return direction;
        }

        public static float GetAngle(Vector2 vector1, Vector2 vector2)
        {
            double dotProduct = GetDot(vector1, vector2);
            double magnitude1 = GetMagnitude(vector1);
            double magnitude2 = GetMagnitude(vector2);

            double cosAngle = dotProduct / (magnitude1 * magnitude2);
            double angleRadians = Math.Acos(cosAngle);
            float angle = (float)Math.Round(GetDegress(angleRadians));
            return angle >= 0 ? angle : 0;
        }

        public static double GetMagnitude(Vector2 vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        public static double GetDot(Vector2 vector1, Vector2 vector2)
        {
            return (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        public static double GetDegress(double value)
        {
            return value * (180 / Math.PI);
        }
    }
}
