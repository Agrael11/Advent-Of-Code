namespace advent_of_code.Year2025.Day03
{
    internal static class Common
    {
        /// <summary>
        /// Finds first highest value in integer array
        /// </summary>
        /// <param name="array">Array in which for search</param>
        /// <param name="start">Start point of search (inclusive)</param>
        /// <param name="end">End point of earch (exclusive)</param>
        /// <returns>Index of first highest value</returns>
        public static int IndexOf_Highest(this int[] array, int start, int end)
        {
            var highestVal = array[start];
            var highestIndex = start;
            for (var i = start; i < end; i++)
            {
                if (array[i] > highestVal)
                {
                    highestVal = array[i];
                    highestIndex = i;
                }
            }
            return highestIndex;
        }
    }
}
