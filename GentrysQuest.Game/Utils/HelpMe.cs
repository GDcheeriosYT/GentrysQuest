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
    }
}
