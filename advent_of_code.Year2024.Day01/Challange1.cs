namespace advent_of_code.Year2024.Day01
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Split input into two lists
            var listLeft = new List<int>();
            var listRight = new List<int>();

            foreach (var line in input.Select(t=>t.Split(' ',StringSplitOptions.RemoveEmptyEntries)))
            {
                listLeft.Add(int.Parse(line[0]));
                listRight.Add(int.Parse(line[1]));
            }

            //Sort the lists
            listLeft = listLeft.Order().ToList();
            listRight = listRight.Order().ToList();

            //Go through lists from start to end, comparing units
            var totalDifference = 0;
            for (var index = 0; index < listLeft.Count; index++)
            {
                var item1 = listLeft[index];
                var item2 = listRight[index];
                totalDifference += Math.Abs(item1 - item2);
            }

            return totalDifference;
        }
    }
}