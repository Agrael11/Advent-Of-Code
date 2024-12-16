namespace advent_of_code.Year2024.Day16
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "#################\r\n#...#...#...#..E#\r\n#.#.#.#.#.#.#.#.#\r\n#.#.#.#...#...#.#\r\n#.#.#.#.###.#.#.#\r\n#...#.#.#.....#.#\r\n#.#.#.#.#.#####.#\r\n#.#...#.#.#.....#\r\n#.#.#####.#.###.#\r\n#.#.#.......#...#\r\n#.#.###.#####.###\r\n#.#.#...#.....#.#\r\n#.#.#.#####.###.#\r\n#.#.#.........#.#\r\n#.#.#.#########.#\r\n#S#.............#\r\n#################";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            (var startX, var startY, var endX, var endY) = Common.Parse(input);

            var score = Dijkstra(startX, startY, endX, endY);

            return score;
        }
        
        //Now this is lot more complex dijkstra, as we are now finding multiple paths of same cost
        private static int Dijkstra(int startX, int startY, int endX, int endY)
        {
            var queue = new PriorityQueue<(int x, int y, int direction), int>();
            queue.Enqueue((startX, startY, 0), 0);

            //This replaces our "Visited" set. Contains prices for every point, all end points we find, and best endpoint cost.
            var prices = new Dictionary<(int, int, int), int>();
            var ends = new HashSet<(int, int, int)>();
            var bestEnd = int.MaxValue;

            //This is for backtracking - contains previous point of each point. We will then crawl backwards from ends to find paths.
            var parents = new Dictionary<(int, int, int), List<(int, int, int)>>();

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var current, out var currentScore);

                //If this path has worse score than our best end score, it means we won't get any better ends.
                if (currentScore > bestEnd)
                {
                    break;
                }

                if (current.x == endX && current.y == endY)
                {
                    //We set best end score (only really matters if this is first end we found, but checking would be slower)
                    bestEnd = currentScore;
                    ends.Add(current);
                }

                foreach (var (nextX, nextY, nextDirection, nextScore) in Common.GetNext(current.x, current.y, current.direction, currentScore))
                {
                    //If our score is better than known score at this point, we found better way to get here.
                    //That means that are previous parents that lead to this point can be removed as they are no longer best paths.
                    //Then we just add current point to this next point's parent list.
                    var knownScore = prices.GetValueOrDefault((nextX, nextY, nextDirection), int.MaxValue);
                    if (nextScore < knownScore)
                    {
                        prices[(nextX, nextY, nextDirection)] = nextScore;
                        queue.Enqueue((nextX, nextY, nextDirection), nextScore);
                        if (parents.TryGetValue((nextX, nextY, nextDirection), out var parList))
                        {
                            parList.Clear();
                        }
                        else
                        {
                            parList = parents[(nextX, nextY, nextDirection)] = new List<(int, int, int)>();
                        }
                        parList.Add((current.x, current.y, current.direction));
                    }
                    //If it's equal - we just add current point to this next point's parent list.
                    else if (nextScore == knownScore)
                    {
                        prices[(nextX, nextY, nextDirection)] = knownScore;
                        if (!parents.TryGetValue((nextX, nextY, nextDirection), out var parList))
                        {
                            parList = new List<(int, int, int)>();
                            parents[(nextX, nextY, nextDirection)] = parList;
                        }
                        parList.Add((current.x, current.y, current.direction));
                    }
                }
            }

            return CountPointsInPaths(parents, ends);
        }

        //Crawls backwards through all paths and counts number of points we hit. I used simple DFS for that
        private static int CountPointsInPaths(Dictionary<(
            int x, int y, int direction), 
            List<(int x, int y, int direction)>> paths,
            HashSet<(int x, int y, int direction)> ends)
        {
            var points = new HashSet<(int, int)>(); //This will hold lits of unique points we visit

            //We added all end points... you know - rotations
            var stack = new Stack<(int x, int y, int direction)>();
            foreach (var end in ends)
            {
                stack.Push(end);
            }

            //And we crawl backwards. adding each point to our list of unique points visited
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                
                points.Add((current.x, current.y));

                if (paths.TryGetValue(current, out var parents))
                {
                    foreach (var parent in parents)
                    {
                        stack.Push(parent);
                    }
                }
            }

            return points.Count;
        }
    }
}