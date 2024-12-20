using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day20
{
    internal class Common
    {

        private static readonly HashSet<(int x, int y)> Walls = new HashSet<(int x, int y)>();
        private static int Width = -1;
        private static int Height = -1;
        private static readonly (int offsetX, int offsetY)[] OFFSETS = [(-1, 0), (+1, 0), (0, -1), (0, +1)];
        private static (int x, int y)[] Path = Array.Empty<(int x, int y)>();
        private static long[,] DistancedPath = new long[0, 0];
        
        public static void Parse(string[] input)
        {
            Walls.Clear();

            Height = input.Length;
            Width = input[0].Length;
            DistancedPath = new long[Width, Height];

            var startX = 0;
            var startY = 0;
            var endX = 0;
            var endY = 0;
            var pathLength = 0;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    switch (input[y][x])
                    {
                        case '#':
                            Walls.Add((x, y));
                            break;
                        case 'S':
                            (startX, startY) = (x, y);
                            pathLength++;
                            break;
                        case 'E':
                            (endX, endY) = (x, y);
                            pathLength++;
                            break;
                        default:
                            pathLength++;
                            break;
                    }
                }
            }
            Path = new (int x, int y)[pathLength];

            FindMainPath(startX, startY, endX, endY, pathLength);
        }

        public static long CountSavesAboveTreshold(int treshold, int maxDistance)
        {
            //Paralelized each of the start points
            var jobs = new List<SingleJob<long>>();

            for (var i = 0; i < Path.Length - 2; i++)
            {
                var copyIndex = i;
                jobs.Add(new SingleJob<long>(() => CountSavesAboveTreshold(treshold, maxDistance, copyIndex)));
            }

            var result = SingleJob<long>.RunParallelized<long>(jobs, jobs.Count/100, (list)=>list.Sum());

            return result.Sum(r => r.Results);
        }

        public static long CountSavesAboveTreshold(int treshold, int maxDistance, int currentIndex)
        {
            var cheatedPaths = 0L;

            var (thisX, thisY) = Path[currentIndex];
            var currentDistance = DistancedPath[thisX, thisY];

            for (var nextIndex = currentIndex + 2; nextIndex < Path.Length; nextIndex++)
            {
                var (otherX, otherY) = Path[nextIndex];
                var manhattan = Math.Abs(thisX - otherX) + Math.Abs(thisY - otherY);

                //Here I'm basically checking if this point is possible to reach by ignoring walls for "maxDistance"
                if (manhattan <= 1 || manhattan > maxDistance) continue;

                //And we calcule how much steps we saved.
                var difference = currentDistance - DistancedPath[otherX, otherY] - manhattan;

                if (difference < treshold)
                    continue;

                //If it's more than treshold we needed we count it as successfuly cheated path
                cheatedPaths++;
            }

            return cheatedPaths;
        }

        //Crawls through the maze to find order in which path goes
        private static void FindMainPath(int startX, int startY, int endX, int endY, int pathLength)
        {
            var previousX = -1;
            var previousY = -1;
            var currentX = startX;
            var currentY = startY;

            //Sets first point in path
            Path[0] = (currentX, currentY);
            DistancedPath[currentX, currentY] = pathLength - 1;

            for (var length = 1; ; length++)
            {
                if (currentX == endX && currentY == endY)
                {
                    break;
                }
                foreach (var (offsetX, offsetY) in OFFSETS)
                {
                    var nextX = currentX + offsetX;
                    var nextY = currentY + offsetY;

                    //We check if next is actual next point on path
                    if (nextX == previousX && nextY == previousY)
                        continue;
                    if (Walls.Contains((nextX, nextY)))
                        continue;

                    //If it is we assign it into path and set it's distance
                    (previousX, previousY) = (currentX, currentY);
                    (currentX, currentY) = (nextX, nextY);

                    Path[length] = (currentX, currentY);
                    DistancedPath[currentX, currentY] = pathLength - length;
                    break;
                }
            }
        }
    }
}
