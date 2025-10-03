namespace advent_of_code.Year2019.Day14
{
    public static class Challange2
    {
        private readonly static long Target = 1000000000000L;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.ParseRecipes(input);

            var previousLow = 1L;
            var previousHigh = 1000000000000L;
            var current = Target / Common.CraftFuel(1);

            while (true)
            {
                var oreUsage = Common.CraftFuel(current);
                if (oreUsage > Target)
                {
                    previousHigh = current;
                    current = (previousLow + current) / 2;
                    continue;
                }
                if (oreUsage < Target)
                {
                    previousLow = current;
                    if (previousHigh - previousLow <= 1)
                    {
                        return previousLow;
                    }
                    current = (previousHigh + current) / 2;
                    continue;
                }
                return current;
            }
        }
    }
}