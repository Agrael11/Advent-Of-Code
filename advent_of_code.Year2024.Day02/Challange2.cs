namespace advent_of_code.Year2024.Day02
{
    public static class Challange2
    {
        private enum StateChange { Falling, Climbing, Wrong };
        private static readonly int MINIMUM = 1;
        private static readonly int MAXIMUM = 3;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var totalSafe = 0;

            //For each list of levels in reports (split by space and convert to integer)
            //This time we convert parse to list so we don't recalculate stuff unnecesseraily
            foreach (var entries in input.Select(
                line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()))
            {
                //We try to remove one level of options
                for (var skippedIndex = 0; skippedIndex < entries.Count; skippedIndex++)
                {
                    var skippedEntries = entries.Where((_, index) => index != skippedIndex);

                    //Same as on part 1, but with one skipepd level.
                    var results = skippedEntries.Zip(skippedEntries.Skip(1), GetChange);

                    //If First item in result is not wrong, and every item is same as first (all climbing or falling) it is Safe.
                    //We count safe only once (Yea i did that one)
                    if (results.First() != StateChange.Wrong && results.All(result => result == results.First()))
                    {
                        totalSafe++;
                        break;
                    }
                }
            }

            return totalSafe;
        }

        /// <summary>
        /// Returns the change state of two numbers
        /// </summary>
        /// <param name="previousLevel">Previous level</param>
        /// <param name="nextLevel">Current level</param>
        /// <returns></returns>
        private static StateChange GetChange(int previousLevel, int nextLevel)
        {
            var change = nextLevel - previousLevel;
            var absChange = Math.Abs(change);
            if (absChange < MINIMUM || absChange > MAXIMUM) return StateChange.Wrong;
            if (change < 0) return StateChange.Falling;
            if (change > 0) return StateChange.Climbing;
            return StateChange.Wrong;
        }
    }
}