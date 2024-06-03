using System;

namespace GentrysQuest.Game.Utils
{
    public class PercentOf(double value, float percent)
    {
        /// <summary>
        /// the value
        /// </summary>
        private double value;

        /// <summary>
        /// the percent
        /// </summary>
        private float percent;

        /// <summary>
        /// returns percent of a value as a double
        /// </summary>
        /// <param name="percentOf">this</param>
        /// <returns>double percent</returns>
        public static implicit operator double(PercentOf percentOf) => percentOf.value * percentOf.percent;

        /// <summary>
        /// returns percent of a value as an int
        /// </summary>
        /// <param name="percentOf">this</param>
        /// <returns>int percent</returns>
        public static implicit operator int(PercentOf percentOf) => (int)Math.Round(percentOf.value * percentOf.percent);
    }
}
