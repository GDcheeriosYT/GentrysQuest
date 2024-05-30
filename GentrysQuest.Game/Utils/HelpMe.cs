namespace GentrysQuest.Game.Utils
{
    /// <summary>
    /// All the helper methods to help me do things because I can't code :)
    /// </summary>
    public static class HelpMe
    {
        /// <summary>
        /// Returns the next value from an array given the index
        /// </summary>
        /// <param name="array">The array</param>
        /// <returns>the next value in the array</returns>
        public static dynamic GetNextValueFromArray(dynamic[] array, ref int currentIndex)
        {
            currentIndex++;
            if (currentIndex > array.Length - 1) currentIndex = 0;
            return array[currentIndex];
        }

        /// <summary>
        /// Function to get scaled levels
        /// </summary>
        /// <param name="difficulty">Target difficulty</param>
        /// <param name="levelRef">Target level</param>
        /// <returns>Scaled level</returns>
        public static int GetScaledLevel(int difficulty, int levelRef)
        {
            int min = difficulty * 20;
            int max = (difficulty + 1) * 20 - 1;
            if (min == 0) min++;

            levelRef += min;
            levelRef += MathBase.RandomInt(-3, 3);

            if (levelRef > max) return max;
            if (levelRef < min) return min;

            return levelRef;
        }
    }
}
