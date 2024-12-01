namespace advent_of_code.Year2024.Day01
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Split input into two lists
            var listLeft = new List<int>();
            var listRight = new List<int>();

            foreach (var line in input.Select(t => t.Split(' ', StringSplitOptions.RemoveEmptyEntries)))
            {
                listLeft.Add(int.Parse(line[0]));
                listRight.Add(int.Parse(line[1]));
            }

            //Count number of repeats of number from first list in second list
            var totalSimilarity = 0L;
            foreach (var number in listLeft)
            {
                totalSimilarity += number * listRight.Count(t => t == number);
            }

            return totalSimilarity;
        }
    }
}