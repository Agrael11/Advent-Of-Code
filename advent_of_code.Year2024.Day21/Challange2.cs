using Visualizers;

namespace advent_of_code.Year2024.Day21
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            Common.Prepare();

            AOConsole.ForegroundColor = AOConsoleColor.Blue;
            AOConsole.WriteLine("PART 2:");

            var total = 0L;
            foreach (var line in input)
            {
                total += Common.CrackTheCode(line, 26); //25 Robots + 1 non-robot I guess.
            }

            return total;
        }
    }
}