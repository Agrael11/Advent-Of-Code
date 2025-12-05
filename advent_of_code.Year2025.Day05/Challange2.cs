namespace advent_of_code.Year2025.Day05
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            (var ranges, _) = Common.Parse(input);


            //Will keep trying to merge ranges until not possible anymore
            while (TryMergeRanges(ranges))
            {
                ;
            }

            return ranges.Sum(range=>range.Length);
        }

        /// <summary>
        /// Tries to merge as many ranges as it can in single pass
        /// </summary>
        /// <param name="ranges">List of ranges</param>
        /// <returns>Success state of merging</returns>
        private static bool TryMergeRanges(List<InclusiveRange> ranges)
        {
            var merged = false;
            for (var rangeIndex = 0; rangeIndex < ranges.Count - 1; rangeIndex++)
            {
                merged |= TryMergeRange(ranges, rangeIndex);
            }
            return merged;
        }

        /// <summary>
        /// Tries to merge range with all other following ranges
        /// </summary>
        /// <param name="ranges">List of rannges</param>
        /// <param name="rangeIndex">Index of the range to be merged</param>
        /// <returns>Success state of merging</returns>
        private static bool TryMergeRange(List<InclusiveRange> ranges, int rangeIndex)
        {
            var merged = false;
            
            var range1 = ranges[rangeIndex];

            //We start at a next range to remove unneeded checks.
            for (var range2Index = rangeIndex + 1; range2Index < ranges.Count; range2Index++)
            {
                var range2 = ranges[range2Index];
                if (range1.TryMerge(range2))
                {
                    merged = true;
                    ranges.RemoveAt(range2Index);
                    range2Index--;
                }
            }

            return merged;
        }
    }
}