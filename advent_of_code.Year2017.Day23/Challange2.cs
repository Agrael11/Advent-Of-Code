namespace advent_of_code.Year2017.Day23
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var b = long.Parse(input[0].Split(' ')[2]) * 100 + 100000;
            var c = b + 17000L;
            var h = 0L;

            for (; b <= c; b += 17)
            {
                for (var d = 2; d * d < b; d++)
                {
                    if (b % d == 0)
                    {
                        h++;
                        break;
                    }
                }
            }

            return h;
        }
    }
}