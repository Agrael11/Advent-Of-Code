namespace advent_of_code.Year2016.Day20
{
    public static class Challange2
    {
        private static readonly long maxValue = 4294967295;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var ranges = new List<Helpers.Range>();

            foreach (var ipRange in input)
            {
                var rangeStrings = ipRange.Split('-');
                var lower = long.Parse(rangeStrings[0]);
                var higher = long.Parse(rangeStrings[1]);
                ranges.Add(new Helpers.Range(lower, higher));
            }
            ranges = Helpers.Range.TryCombine(ranges);

            var result = maxValue + 1;

            foreach (var range in ranges)
            {
                result -= range.Length;
            }

            return result;
        }
    }
}