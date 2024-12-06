namespace advent_of_code.Year2024.Day06
{
    internal static class Common
    {
        private readonly static (int X, int Y)[] OffsetTable = [(0, -1), (+1, 0), (0, +1), (-1, 0)];

        internal static char[][] Map { get; private set; } = Array.Empty<char[]>();

        internal static int Width { get; set; } = 0;
        internal static int Height { get; set; } = 0;
        internal static (int X, int Y, int Direction) GuardState = (0, 0, 0);

        internal static void Parse(string[] input)
        {
            Map = input.Select(x => x.ToCharArray()).ToArray();
            Height = Map.Length;
            if (Height > 0)
            {
                Width = Map[0].Length;

                //We search for start position of guard
                for (var y = 0; y < Height; y++)
                {
                    if (input[y].Contains('^'))
                    {
                        GuardState.Y = y;
                        GuardState.X = input[y].IndexOf('^');
                        break;
                    }
                }
            }
            GuardState.Direction = 0;
        }

        internal static HashSet<(int X, int Y)> GetVisitedLocations()
        {
            var visitedPositions = new HashSet<(int X, int Y)>();

            for (; ; )
            {
                visitedPositions.Add((GuardState.X, GuardState.Y));
                (var nextX, var nextY) = (GuardState.X + OffsetTable[GuardState.Direction].X, GuardState.Y + OffsetTable[GuardState.Direction].Y);
                
                //If we are out of bonds, we left the area
                if (nextX < 0 || nextY < 0 || nextX >= Width || nextY >= Height)
                {
                    break;
                }

                //If we hit the wall we change direction
                if (Map[nextY][nextX] == '#')
                {
                    GuardState.Direction = (GuardState.Direction + 1) % 4;
                    continue;
                }

                //Otherwise we move to next location
                GuardState.X = nextX;
                GuardState.Y = nextY;
            }

            return visitedPositions;
        }
    }
}
