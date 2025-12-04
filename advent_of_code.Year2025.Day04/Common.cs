
namespace advent_of_code.Year2025.Day04
{
    internal static class Common
    {
        private static readonly char PaperChar = '@';
        private static readonly int AccessibilityThreashold = 4;

        /// <summary>
        /// Parses input into set of papers
        /// </summary>
        /// <param name="input">Input map</param>
        /// <returns>Set of papers</returns>
        public static HashSet<(int X, int Y)> ParseInput(string[] input)
        {
            var papers = new HashSet<(int X, int Y)>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == PaperChar) papers.Add((x, y));
                }
            }

            return papers;
        }

        /// <summary>
        /// Checks if paper in set is accessible
        /// </summary>
        /// <param name="papers">Input Paper set</param>
        /// <param name="position">Position of paper</param>
        /// <returns>Accessiblity Status</returns>
        public static bool IsAccessible(this HashSet<(int X, int Y)> papers, (int X, int Y) position)
        {
            return papers.CountAround(position) < AccessibilityThreashold;
        }

        /// <summary>
        /// Counts number of papers surrounding the selected position
        /// </summary>
        /// <param name="papers">Input Paper set</param>
        /// <param name="position">Position of paper</param>
        /// <returns>Amount of papers in 8 positions around</returns>
        private static int CountAround(this HashSet<(int X, int Y)> papers, (int X, int Y) position)
        {
            var count = 0;
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    //We do not count ourselves
                    if (xOffset == 0 && yOffset == 0) continue;
                    count += papers.Contains((position.X + xOffset, position.Y + yOffset)) ? 1 : 0;
                }
            }
            return count;
        }
    }
}
