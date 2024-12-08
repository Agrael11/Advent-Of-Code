namespace advent_of_code.Year2024.Day08
{
    public static class Challange2
    {
        private static int Width = 0;
        private static int Height = 0;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var antennas = new Dictionary<char, List<(int X, int Y)>>();
            Height = input.Length;
            Width = input[0].Length;

            //We parse the input grid into dictionary
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (input[y][x] == '.')
                        continue;

                    if (!antennas.TryGetValue(input[y][x], out var list))
                    {
                        list = new List<(int X, int Y)>(); ;
                        antennas[input[y][x]] = list;
                    }

                    list.Add((x, y));
                }
            }

            //We do the same iteration logic as in part 1.
            var antinodes = new HashSet<(int x, int y)>();
            foreach (var frequency in antennas.Keys)
            {
                foreach (var (antenna1X, antenna1Y) in antennas[frequency])
                {
                    foreach (var (antenna2X, antenna2Y) in antennas[frequency])
                    {
                        if (antenna1X == antenna2X && antenna1Y == antenna2Y)
                        {
                            continue;
                        }

                        //Except this function is bit different (see below)
                        AddAntinodes(antenna1X, antenna1Y, antenna2X, antenna2Y, ref antinodes);
                    }
                }
            }

            return antinodes.Count;
        }

        public static void AddAntinodes(int antenna1X, int antenna1Y, int antenna2X, int antenna2Y, ref HashSet<(int x, int y)> antinodes)
        {
            //We again calculate vector of movement from antenna 1 to antenna 2.
            var xDist = antenna2X - antenna1X;
            var yDist = antenna2Y - antenna1Y;

            //But this time we created copy of antenna 1 position.
            var xNext = antenna1X;
            var yNext = antenna1Y;

            //While we're in bounds
            while (xNext >= 0 && xNext < Width && yNext >= 0 && yNext < Height)
            {
                //We add the new point into list of antinodes
                //And created next one by moving position by the vector
                antinodes.Add((xNext, yNext));
                xNext += xDist;
                yNext += yDist;

                //Note - this adds position of first antenna and second antenna too! That is what we want
                //First antenna is added by the first point being set to it's coordinates when declaring it's variables
                //and because we are adding before changing position.
            }

            //Now we reset the next point, and move it backwards instead. this also add "previous points" on imaginary line
            xNext = antenna1X - xDist;
            yNext = antenna1Y - yDist;
            while (xNext >= 0 && xNext < Width && yNext >= 0 && yNext < Height)
            {
                antinodes.Add((xNext, yNext));
                xNext -= xDist;
                yNext -= yDist;
            }
        }
    }
}