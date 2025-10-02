
namespace advent_of_code.Year2019.Day09
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",").Select(long.Parse).ToArray();

            if (!Common.TryGetOutput(input, 2, out var results))
            {
                foreach (var result in results)
                {
                    Visualizers.AOConsole.WriteLine(result.ToString());
                }
                return -1;
            }

            return results[0];
        }
    }
}