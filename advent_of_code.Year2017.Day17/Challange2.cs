namespace advent_of_code.Year2017.Day17
{
    public static class Challange2
    {
        private static readonly int Count = 50_000_000;

        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            var length = 1;
            var after0 = 0;
            var pos = 0;

            for (var i = 1; i <= Count; i++)
            {
                pos = (pos + input) % length + 1;
                if (pos == 1) { after0 = i; }
                length++;
            }

            return after0;
        }
    }
}