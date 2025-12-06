namespace advent_of_code.Year2025.Day06
{
    internal static class Common
    {
        /// <summary>
        /// Extension so i can easily multiply numbers in list
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static long Multiply(this IEnumerable<long> numbers)
        {
            var result = 1L;
            foreach (var number in numbers)
            {
                result *= number;
            }
            return result;
        }
    }
}
