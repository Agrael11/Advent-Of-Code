namespace advent_of_code.Year2024.Day05
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            (var rules, var startOfData) = Common.Parse(input);

            var checkSum = 0;

            for (var i = startOfData; i < input.Length; i++)
            {
                var printJob = input[i].Split(',').Select(int.Parse).ToArray();

                //If printing job order is NOT okay
                if (!Common.PrintJobAdheresTorRules(rules, printJob))
                {
                    //We order the printJob using custom comparer and add it's midpoint to checksum.
                    var orderedPrintJob = printJob.OrderBy(item => item, Comparer<int>.Create((x, y) => Comparer(x, y, rules))).ToArray();
                    checkSum += orderedPrintJob[orderedPrintJob.Length / 2];
                }
            }

            return checkSum;
        }

        /// <summary>
        /// Compares two pages by ordering rules
        /// </summary>
        /// <param name="page1">First Page</param>
        /// <param name="page2">Second Page</param>
        /// <param name="rules">Rules</param>
        /// <returns>
        /// If page1 is supposed to come before page2, returns -1
        /// If pages are exactly same, returns 0
        /// If page1 is supposed to come after page2, returns +1
        /// </returns>
        private static int Comparer(int page1, int page2, Dictionary<int, List<int>> rules)
        {
            if (page1 == page2) return 0;

            if (rules.TryGetValue(page2, out var pageRules) && pageRules.Contains(page1)) return -1;

            return +1;
        }
    }
}