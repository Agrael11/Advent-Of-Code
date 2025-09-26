namespace advent_of_code.Year2019.Day02
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(",").Select(int.Parse).ToArray();

            if (!Common.TryRunMachine(12, 2, input, out var result))
            {
                return -1;
            }
            return result;
        }
    }
}