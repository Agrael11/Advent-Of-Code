namespace advent_of_code.Year2024.Day10
{
    //Everythign is same as in part 1 except for DFS
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var startPoints = Common.Parse(input);

            var totalScore = 0L;

            foreach (var (startX, startY) in startPoints)
            {
                totalScore += DFSCountScore(startX, startY);
            }

            return totalScore;
        }

        //Almost as in part 1, but we don't keep track of visited cells
        //That means we explore all possible paths to each 9
        public static long DFSCountScore(int x, int y)
        {
            var stack = new Stack<(int x, int y)>();
            stack.Push((x, y));

            var score = 0L;

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                foreach ((var nextX, var nextY, var value) in Common.GetNext(current.x, current.y))
                {
                    if (value == 9)
                    {
                        score++;
                        continue;
                    }

                    stack.Push((nextX, nextY));
                }
            }

            return score;
        }
    }
}