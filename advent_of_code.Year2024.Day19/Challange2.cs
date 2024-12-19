using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day19
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var designs = Common.Parse(input);

            var possible = 0L;
            foreach (var design in designs)
            {
                possible += Common.CountPossiblePatterns(design);
            }
            return possible;
        }
    }
}