namespace advent_of_code.Year2017.Day03
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));
            input--;
            (var x, var y) = ValueHelper.GetAbsXY(input);
            return x + y;
        }
    }
}