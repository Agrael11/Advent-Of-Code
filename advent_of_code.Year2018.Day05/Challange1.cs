namespace advent_of_code.Year2018.Day05
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            return Common.Compact(input.ToList());
        }
    }
}