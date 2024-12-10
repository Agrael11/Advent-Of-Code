namespace advent_of_code.Year2024.Day10
{
    internal static class Common
    {
        private static int Width = -1;
        private static int Height = -1;
        private static int[,] Grid = new int[0, 0];
        private static readonly (int offsetX, int offsetY)[] Offsets = [(+1, 0), (-1, 0), (0, +1), (0, -1)];

        internal static List<(int x, int y)> Parse(string[] input)
        {
            Height = input.Length;
            Width = input[0].Length;
            Grid = new int[Width, Height];
            
            //This converts every digit to integer, and identifies start points (0s)
            var StartPoints = new List<(int x, int y)>();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var num = input[y][x] - '0';
                    Grid[x, y] = num;
                    if (num != 0)
                    {
                        continue;
                    }
                    StartPoints.Add((x, y));
                }
            }

            return StartPoints;
        }

        public static IEnumerable<(int x, int y, int result)> GetNext(int currentX, int currentY)
        {
            //We move through all directions and get next points
            foreach (var (offsetX, offsetY) in Offsets)
            {
                var nextX = currentX - offsetX;
                var nextY = currentY - offsetY;
                
                if (nextX < 0 || nextY < 0 || nextX >= Width || nextY >= Height)
                {
                    continue;
                }

                var nextPoint = Grid[nextX, nextY];
                
                //Next points is only valid if it is 1 higher from previous
                if (nextPoint - Grid[currentX, currentY] == 1) yield return (nextX, nextY, nextPoint);
            }
        }
    }
}
