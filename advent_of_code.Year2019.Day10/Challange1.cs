namespace advent_of_code.Year2019.Day10
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var asteroids = Common.ParseInput(input);

            return Common.FindBestStation(asteroids).visible;
        }
    }
}