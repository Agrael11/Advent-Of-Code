namespace advent_of_code.Year2024.Day15
{
    public static class Challange1
    {
        private enum EntityType { Empty, Box, Wall };

        private static int Width = 0;
        private static int Height = 0;
        private static EntityType[,] Map = new EntityType[0, 0];
        private static (int X, int Y) Player = (0,0);

        private static readonly (int OffsetX, int OffsetY)[] Offsets = [(-1, 0), (+1, 0), (0, -1), (0, +1)];

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var instructions = Parse(input);

            foreach (var instruction in instructions)
            {
                TryMove(instruction);
            }

            var total = 0L;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Map[x,y] != EntityType.Box)
                        continue;

                    total += (100 * y + x);
                }
            }

            return total;
        }

        private static bool TryMove(int direction)
        {
            var nextX = Player.X + Offsets[direction].OffsetX;
            var nextY = Player.Y + Offsets[direction].OffsetY;
            var itemAt = Map[nextX, nextY];
            if (itemAt == EntityType.Wall) return false;

            if (itemAt == EntityType.Box && !TryPush(direction)) return false;

            Player.X = nextX;
            Player.Y = nextY;
            return true;
        }

        //We try to push
        private static bool TryPush(int direction)
        {
            (var offsetX, var offsetY) = Offsets[direction];
            var startX = Player.X + offsetX;
            var startY = Player.Y + offsetY;
            var currentX = startX;
            var currentY = startY;
            for(; ; )
            {
                currentX += offsetX;
                currentY += offsetY;
                var currentBox = Map[currentX, currentY];
                if (currentBox == EntityType.Wall)
                    return false;
                if (currentBox == EntityType.Empty)
                    break;
            }
            Map[startX, startY] = EntityType.Empty;
            Map[currentX, currentY] = EntityType.Box;
            return true;
        }

        /// <summary>
        /// Parses the input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static int[] Parse(string[] input)
        {
            //Here we find end of grid (and start of instructions therefore)
            for (var y = 0; y < input.Length; y++)
            {
                var line = input[y];
                if (string.IsNullOrWhiteSpace(line))
                {
                    Height = y;
                    break;
                }
            }

            //And create new map
            Width = input[0].Length;
            Map = new EntityType[Width, Height];


            //Then we parse it - O is Box, # is wall, @ is player.
            for (var y = 0; y < Height; y++)
            {
                var line = input[y];
                for (var x = 0; x < Width; x++)
                {
                    switch (line[x])
                    {
                        case 'O':
                            Map[x, y] = EntityType.Box;
                            break;
                        case '#':
                            Map[x, y] = EntityType.Wall;
                            break;
                        case '@':
                            Player = (x, y);
                            break;
                    }
                }
            }

            //And convert the rest of input (instructions) into offset info
            return string.Join("", input[(Height+1)..]).Select(c => {
                return c switch
                {
                    '<' => 0,
                    '>' => 1,
                    '^' => 2,
                    'v' => 3,
                    _ => throw new Exception($"Unknown symbol {c}"),
                };
            }).ToArray();
        }
    }
}