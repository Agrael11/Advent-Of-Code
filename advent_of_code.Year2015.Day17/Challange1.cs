namespace advent_of_code.Year2015.Day17
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var numbers = new List<int>();
            foreach (var line in input)
            {
                numbers.Add(int.Parse(line));
            }

            return GetCombinations(ref numbers, 0, 0);
        }

        public static int GetCombinations(ref List<int> numbers, int startIndex, int total)
        {
            if (total > 150)
            {
                return 0;
            }

            if (total == 150)
            {
                return 1;
            }

            var sum = 0;
            for (var i = startIndex; i < numbers.Count; i++)
            {
                var number = numbers[i];
                numbers.RemoveAt(i);
                sum += GetCombinations(ref numbers, i, total + number);
                numbers.Insert(i, number);
            }

            return sum;
        }
    }
}