namespace advent_of_code.Year2024.Day18
{
    public static class Challange1
    {
        private static readonly int WIDTH = 71;
        private static readonly int HEIGHT = 71;
        private static readonly int MEMORYAREA = 1024;
        private static readonly (int OffsetX, int OffsetY)[] OFFSETS = [(-1, 0), (+1, 0), (0, -1), (0, +1)];

        private static readonly HashSet<(int x, int y)> Bytes = new HashSet<(int x, int y)>();

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(line => line.Split(',')).ToArray();

            Bytes.Clear();
            for (var i = 0; i < MEMORYAREA; i++)
            {
                var position = input[i];
                Bytes.Add((int.Parse(position[0]), int.Parse(position[1])));
            }

            return BFS();
        }

        //I did this so many times this year
        private static int BFS()
        {
            var visited = new HashSet<(int x, int y)>();
            var queue = new Queue<(int x, int y, int steps)>();
            queue.Enqueue((0, 0, 0));

            while (queue.Count > 0)
            {
                (var currentX, var currentY, var currentSteps) = queue.Dequeue();
                if (currentX == WIDTH - 1 && currentY == HEIGHT - 1) return currentSteps;

                foreach (var (OffsetX, OffsetY) in OFFSETS)
                {
                    var nextX = currentX + OffsetX;
                    var nextY = currentY + OffsetY;
                    
                    if (nextX < 0 || nextX >= WIDTH || nextY < 0 || nextY >= HEIGHT)
                        continue;

                    if (Bytes.Contains((nextX,nextY)))
                        continue;

                    if (!visited.Add((nextX, nextY)))
                        continue;

                    queue.Enqueue((nextX, nextY, currentSteps + 1));
                }
            }

            return -1;
        }
    }
}