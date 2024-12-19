namespace advent_of_code.Year2024.Day19
{
    public static class Challange1
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var designs = Common.Parse(input);

            var possible = 0L;
            foreach (var design in designs)
            {
                possible += (Common.CountPossiblePatterns(design) != 0) ? 1 : 0;
            }
            return possible;
        }
    }
}