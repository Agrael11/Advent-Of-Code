using Visualizers;

namespace advent_of_code.Year2024.Day21
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            AOConsole.Clear();
            Common.Prepare();

            AOConsole.ForegroundColor = AOConsoleColor.Blue;
            AOConsole.WriteLine("PART 1:");

            var total = 0L;
            foreach (var line in input)
            {
                total += Common.CrackTheCode(line, 3); 
            }

            AOConsole.WriteLine("\n\n");

            return total;
        }

        
    }
}