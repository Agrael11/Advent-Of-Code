namespace advent_of_code.Year2024.Day06
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Parse(input);

            return Common.GetVisitedLocations().Count;
        }
    }
}