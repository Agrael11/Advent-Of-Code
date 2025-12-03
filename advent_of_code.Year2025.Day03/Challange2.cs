namespace advent_of_code.Year2025.Day03
{
    public static class Challange2
    {
        private static readonly int ExpectedBatteryCount = 12;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(item => item.Select(ch => int.Parse(ch.ToString())).ToArray());

            var sum = 0L;

            foreach (var bank in input)
            {
                var index = -1;
                var value = 0L;
                
                //We search for count of batteries required per bank
                //We are going backwards to simplify math later
                for (var i = ExpectedBatteryCount-1; i >= 0; i--)
                {
                    //This makes sure we find largest baterry that is safely
                    //far from end so we can find enough remaining ones.
                    var endOffset = bank.Length - i;

                    //For each one we find next highest digit (remembering index of last one),
                    //and add them to value of the combined pack.
                    index = bank.IndexOf_Highest(index + 1, endOffset);
                    value = value * 10 + bank[index];
                }

                sum += value;
            }

            return sum;
        }
    }
}