namespace advent_of_code.Year2019.Day01
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            return input.Select(t => Common.CalculateFuel(int.Parse(t), true)).Sum();
        }
    }
}