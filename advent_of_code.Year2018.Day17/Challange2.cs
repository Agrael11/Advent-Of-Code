namespace advent_of_code.Year2018.Day17
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            Common.Reset(input);
            return Common.Run().still;
        }
    }
}