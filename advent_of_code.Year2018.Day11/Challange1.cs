namespace advent_of_code.Year2018.Day11
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            Common.Setup(int.Parse(input));

            var (x, y, _) = Common.GetHighestAtSize(3);

            return $"{x},{y}";
        }
    }
}