namespace advent_of_code.Year2024.Day24
{
    public static class Challange1
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Parse(input);
            Common.Solve();

            return Common.GetResult('z');
        }
    }
}