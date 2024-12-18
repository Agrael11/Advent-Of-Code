namespace advent_of_code.Year2024.Day18
{
    public static class Challange2
    {
        private static readonly int WIDTH = 71;
        private static readonly int HEIGHT = 71;
        private static readonly (int OffsetX, int OffsetY)[] OFFSETS = [(-1, 0), (+1, 0), (0, -1), (0, +1)];

        private static readonly List<(int x, int y)> Bytes = new List<(int x, int y)>();

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(line => line.Split(',')).ToArray();

            Bytes.Clear();
            foreach (var position in input)
            {
                Bytes.Add((int.Parse(position[0]), int.Parse(position[1])));
            }

            //I made binary search for this... and now seeing the result of naive approach... whyyy
            //We are finding first point that is blocked by checking only half points between two points
            //Lowest passing and Highest blocked
            var currentIndex = Bytes.Count - 1;
            var last = currentIndex;
            var highest = Bytes.Count - 1;
            var lowest = 0;

            for (; ; )
            {
                var result = BFS(Bytes[0..(currentIndex + 1)].ToHashSet());
                //Sure, converting to hashset is not fastest, but it still saved 30ms over IndexOf
                //And since each is visited once no need to precompute

                if (result)
                {
                    lowest = currentIndex;
                    last = currentIndex;
                    currentIndex = lowest + (highest - lowest) / 2;
                    if (currentIndex == last)
                    {
                        //We increment by one since next one is the "blocking one"
                        currentIndex++;
                        break;
                    }
                }
                else
                {
                    highest = currentIndex;
                    last = currentIndex;
                    currentIndex = lowest + (highest - lowest) / 2;
                    if (currentIndex == last)
                    {
                        break;
                    }
                }
            }

            var (byteX, byteY) = Bytes[currentIndex];
            return $"{byteX},{byteY}";

            /* I was so tempted to test this!... and it is actually really fast. 1.5 second.
             * What am I missing in this problem?
             * Is it suppposed to be this easy?
            
            var byteset = new HashSet<(int x, int y)>();
            for (var i = 0; i < Bytes.Count; i++)
            {
                byteset.Add(Bytes[i]);
                if (!BFS(byteset))
                {
                    var (bX, bY) = Bytes[i];
                    return $"{bX},{bY}";
                }
            }
            return "NONE";
            
             * Now I wonder... how fast the backwards would be?... 3ms...
             * SERIOUSLYYYYYY?!

            var byteset = Bytes.ToHashSet();
            for (var i = Bytes.Count - 1; i >= 0; i--)
            {
                if (BFS(byteset))
                {
                    var (bX, bY) = Bytes[i+1];
                    return $"{bX},{bY}";
                }
                byteset.Remove(Bytes[i]);
            }
            return "NONE?"; */
        }

        // Same BFS as Part 1, but we have custom set of bytes to check
        private static bool BFS(HashSet<(int x, int y)> bytes)
        {
            var visited = new HashSet<(int x, int y)>();
            var queue = new Queue<(int x, int y, int steps)>();
            queue.Enqueue((0, 0, 0));

            while (queue.Count > 0)
            {
                (var currentX, var currentY, var currentSteps) = queue.Dequeue();
                if (currentX == WIDTH - 1 && currentY == HEIGHT - 1) return true;

                foreach (var (OffsetX, OffsetY) in OFFSETS)
                {
                    var nextX = currentX + OffsetX;
                    var nextY = currentY + OffsetY;

                    if (nextX < 0 || nextX >= WIDTH || nextY < 0 || nextY >= HEIGHT)
                        continue;

                    if (bytes.Contains((nextX, nextY)))
                        continue;

                    if (!visited.Add((nextX, nextY)))
                        continue;

                    queue.Enqueue((nextX, nextY, currentSteps + 1));
                }
            }

            return false;
        }
    }
}