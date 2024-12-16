namespace advent_of_code.Year2024.Day16
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "###############\r\n#.......#....E#\r\n#.#.###.#.###.#\r\n#.....#.#...#.#\r\n#.###.#####.#.#\r\n#.#.#.......#.#\r\n#.#.#####.###.#\r\n#...........#.#\r\n###.#.#####.#.#\r\n#...#.....#.#.#\r\n#.#.#.###.#.#.#\r\n#.....#...#.#.#\r\n#.###.#.#.#.#.#\r\n#S..#.....#...#\r\n###############";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            (var startX, var startY, var endX, var endY) = Common.Parse(input);

            var score = Dijkstra(startX, startY, endX, endY);

            return score;
        }

        //This is pretty simple dijsktra. find best path by checking every next step from currently most cost-effective point.
        private static int Dijkstra(int startX, int startY, int endX, int endY)
        {
            var queue = new PriorityQueue<(int x, int y, int direction), int>();
            queue.Enqueue((startX, startY, 0), 0);
            var visited = new HashSet<(int, int, int)>();

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var current, out var currentScore);

                if (current.x == endX && current.y == endY)
                {
                    return currentScore;
                }

                foreach (var (nextX, nextY, nextDirection, nextScore) in Common.GetNext(current.x, current.y, current.direction, currentScore))
                {
                    if (visited.Add((nextX, nextY, nextDirection)))
                    {
                        queue.Enqueue((nextX, nextY, nextDirection), nextScore);
                    }
                }
            }

            return -1;
        }
    }
}