namespace advent_of_code.Year2016.Day20
{
    public static class Challange1
    {
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

            var lowest = 0L;
            foreach (var range in ranges)
            {
                if (range.InRange(lowest)) lowest = range.End + 1;
            }

            return lowest;
        }
    }
}