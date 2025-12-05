namespace advent_of_code.Year2025.Day05
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            //We just parse the input
            (var ranges, var ids) = Common.Parse(input);

            //And count the number of IDs that are within ranges
            return ids.Count(id=>IsIDInRanges(ranges, id));
        }

        /// <summary>
        /// Checkes if ID is in any of the ranges
        /// </summary>
        /// <param name="ranges"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static bool IsIDInRanges(List<InclusiveRange> ranges, long id)
        {
            return ranges.Any(range=>range.Includes(id));
        }
    }
}