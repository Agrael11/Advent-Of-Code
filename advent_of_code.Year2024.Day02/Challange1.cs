namespace advent_of_code.Year2024.Day02
{
    public static class Challange1
    {
        private enum StateChange { Falling, Climbing, Wrong };
        private static readonly int MINIMUM = 1;
        private static readonly int MAXIMUM = 3;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            var totalSafe = 0;

            //For each list of levels in reports (split by space and convert to integer)
            foreach (var entries in input.Select(   
                line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse) ))
            {
                //Zip - exectures function first element of first IEnumerable to first array of second IEnumerable
                //Skip - skips x elements from start of IEnumerable
                //Therefore we are pairing first with second, second with third, etc.
                //Fucntion used on them is "GetChange" which gives us resulting change - climbing, falling or wrong - wrong if it's out of allowed level bounds
                var results = entries.Zip(entries.Skip(1), GetChange);

                //If First item in result is not wrong, and every item is same as first (all climbing or falling) it is Safe
                if (results.First() != StateChange.Wrong && results.All(result => result == results.First()))
                {
                    totalSafe++;
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