namespace advent_of_code.Year2015.Day17
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var numbers = new List<int>();
            foreach (var line in input)
            {
                numbers.Add(int.Parse(line));
            }

            var minUsedCups = int.MaxValue;
            var minCombinations = 0;
            GetCombinations(ref numbers, 0, 0, 0, ref minUsedCups, ref minCombinations);
            return minCombinations;
        }

        public static void GetCombinations(ref List<int> numbers, int startIndex, int total, int usedCups, ref int minUsedCups, ref int minCombinations)
        {
            if (usedCups > minUsedCups)
            {
                return;
            }
            if (total > 150)
            {
                return;
            }
            if (total == 150)
            {
                if (usedCups < minUsedCups)
                {
                    minUsedCups = usedCups;
                    minCombinations = 1;
                }
                else if (usedCups == minUsedCups)
                {
                    minCombinations++;
                }
                return;
            }

            for (var i = startIndex; i < numbers.Count; i++)
            {
                var number = numbers[i];
                numbers.RemoveAt(i);
                GetCombinations(ref numbers, i, total + number, usedCups + 1, ref minUsedCups, ref minCombinations);
                numbers.Insert(i, number);
            }

            return;
        }
    }
}
