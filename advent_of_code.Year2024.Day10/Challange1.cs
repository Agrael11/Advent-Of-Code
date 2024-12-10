namespace advent_of_code.Year2024.Day10
{
    public static class Challange1
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var startPoints = Common.Parse(input);

            //We combine score of each path
            var totalScore = 0L;
            foreach (var (startX, startY) in startPoints)
            {
                totalScore += DFSCountScore(startX, startY);
            }

            return totalScore;
        }

        //Counts score (number of 9s reachable) from start point. Uses simple DFS
        public static long DFSCountScore(int x, int y)
        {
            var stack = new Stack<(int x, int y)>();
            stack.Push((x, y));
            
            //This removes re-evaluating already visited points
            //Therefore less paths to explore and each 9 is counted only once
            var visited = new HashSet<(int x, int y)>();
            visited.Add((x, y));
            
            
            var nines = 0L;

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                foreach ((var nextX, var nextY, var value) in Common.GetNext(current.x, current.y))
                {
                    if (!visited.Add((nextX, nextY)))
                    {
                        continue;
                    }
                 
                    if (value == 9) //If next point is 9 we add count up
                    {
                        nines++;
                        continue;
                    }

                    stack.Push((nextX, nextY));
                }
            }

            return nines;
        }
    }
}