namespace advent_of_code.Year2024.Day16
{
    internal static class Common
    {
        public static bool[,] Map { get; private set; } = new bool[0, 0];
        public static int Width { get; private set; } = 0;
        public static int Heigth { get; private set; } = 0;

        private static readonly (int OffsetX, int OffsetY)[] OFFSETS = [(+1, 0), (0, +1), (-1, 0), (0, -1)];

        //Will get next points to explore - including rotation and direction.
        public static IEnumerable<(int x, int y, int direction, int score)> GetNext(int x, int y, int direction, int score)
        {
            //Will get next point in current direction
            var nextX = x + OFFSETS[direction].OffsetX;
            var nextY = y + OFFSETS[direction].OffsetY;
            if (nextX >= 0 && nextY >= 0 && nextX < Width && nextY < Heigth)
            {
                if (!Map[nextX, nextY]) yield return (nextX, nextY, direction, score + 1);
            }

            //Will get next point if we rotate by 1clockwise (i think)
            var nextDir = (direction + 1) % 4;
            nextX = x + OFFSETS[nextDir].OffsetX;
            nextY = y + OFFSETS[nextDir].OffsetY;
            if (nextX >= 0 && nextY >= 0 && nextX < Width && nextY < Heigth)
            {
                if (!Map[nextX, nextY]) yield return (nextX, nextY, nextDir, score + 1001);
            }

            //Will get next point if we rotate by 1counter-clockwise (i think)
            nextDir = (direction + 3) % 4;
            nextX = x + OFFSETS[nextDir].OffsetX;
            nextY = y + OFFSETS[nextDir].OffsetY;
            if (nextX >= 0 && nextY >= 0 && nextX < Width && nextY < Heigth)
            {
                if (!Map[nextX, nextY]) yield return (nextX, nextY, nextDir, score + 1001);
            }

            //We do not need to check rotating by 2 as that would lead is to previous point - not efficient
        }

        //Simple Parsing
        public static (int startX, int startY, int endX, int endY) Parse(string[] input)
        {
            Heigth = input.Length;
            Width = input[0].Length;
            Map = new bool[Width, Heigth];

            (var startX, var startY) = (0, 0);
            (var endX, var endY) = (0, 0);

            for (var y = 0; y < Heigth; y++)
            {
                var line = input[y];
                for (var x = 0; x < Width; x++)
                {
                    switch (line[x])
                    {
                        case 'S':
                            (startX, startY) = (x, y);
                            break;
                        case 'E':
                            (endX, endY) = (x, y);
                            break;
                        case '#':
                            Map[x, y] = true;
                            break;
                    }
                }
            }

            return (startX, startY, endX, endY);
        }
    }
}
