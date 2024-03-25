using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day13
{
    public static class Challange2
    {
        private static long favNum;
        private static readonly int maxSteps = 50;

        private static readonly HashSet<string> visited = new HashSet<string>();
        private static readonly PriorityQueue<(int x, int y), int> queue = new PriorityQueue<(int x, int y), int>();

        public static int DoChallange(string inputData)
        {
            favNum = long.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            visited.Clear();
            queue.Clear();

            var startPosition = (1, 1);

            FindAll(startPosition);

            return visited.Count;
        }

        public static void FindAll((int x, int y) startState)
        {
            queue.Enqueue(startState, 0);

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var current, out var currentSteps);
                visited.Add(current.x + "-" + current.y);

                if (currentSteps >= maxSteps) return;

                foreach (var (nextPosition, cost) in GetNext(current))
                {
                    if (visited.Contains(nextPosition.x + "-" + nextPosition.y)) continue;

                    queue.Enqueue(nextPosition, currentSteps + cost);
                }
            }
        }

        public static IEnumerable<((int x, int y) nextPosition, int cost)> GetNext((int x, int y) current)
        {
            for (var yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (var xOffset = -1; xOffset <= 1; xOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;
                    if (xOffset != 0 && yOffset != 0) continue;
                    var rx = current.x + xOffset;
                    var ry = current.y + yOffset;
                    if (rx < 0 || ry < 0) continue;
                    if (IsWall(rx, ry)) continue;
                    yield return ((rx, ry), 1);
                }
            }
        }

        private static bool IsWall(int x, int y)
        {
            var math = ((x + (2 * y) + 3) * x) + ((y + 1) * y) + favNum;

            return MathHelpers.BitCount(math) % 2 == 1;
        }
    }
}