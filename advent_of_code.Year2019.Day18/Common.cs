namespace advent_of_code.Year2019.Day18
{
    internal class Common
    {
        public readonly struct Position(int x, int y)
        {
            public readonly int X = x;
            public readonly int Y = y;

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
        }

        private static readonly (int X, int Y)[] Directions =
        {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
        };
        

        internal static List<(int steps, Position position, ulong key)> SearchForKeys(int startX, int startY, ulong keyMask, Map map)
        {
            var foundKeys = new List<(int steps, Position position, ulong key)>();
            var queue = new Queue<(int x, int y, int steps)>();
            queue.Enqueue((startX, startY, 0));
            var visited = new HashSet<(int x, int y)>();
            while (queue.Count > 0)
            {
                var (currentX, currentY, currentSteps) = queue.Dequeue();
                if (!visited.Add((currentX, currentY)))
                {
                    continue;
                }
                foreach (var (X, Y) in Directions)
                {
                    var nextX = currentX + X;
                    var nextY = currentY + Y;
                    var nextSteps = currentSteps + 1;
                    var tile = map[nextX, nextY];
                    if (tile.TileType == MapTileType.Wall)
                    {
                        continue;
                    }
                    if (tile.TileType == MapTileType.Door)
                    {
                        if ((keyMask & tile.KeyID) == 0)
                        {
                            continue;
                        }
                    }
                    if (tile.TileType == MapTileType.Key)
                    {
                        if ((keyMask & tile.KeyID) == 0)
                        {
                            foundKeys.Add((nextSteps, new Position(nextX, nextY), tile.KeyID));
                            continue;
                        }
                    }
                    queue.Enqueue((nextX, nextY, nextSteps));
                }
            }
            return foundKeys;
        }
    }
}
