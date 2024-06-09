namespace GentrysQuest.Game.Utils
{
    /// <summary>
    /// Because we work in milliseconds I'll make a usefull class to make this easier
    /// </summary>
    /// <param name="time">seconds</param>
    public class Second(double time)
    {
        /// <summary>
        /// The value
        /// </summary>
        private double value = time;

        /// <summary>
        /// Converts into milliseconds
        /// </summary>
        /// <param name="second">instance</param>
        /// <returns>milliseconds</returns>
        public static implicit operator int(Second second) => (int)(second.value * 1000);
    }
}
