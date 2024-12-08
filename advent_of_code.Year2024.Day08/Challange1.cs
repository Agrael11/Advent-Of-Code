namespace advent_of_code.Year2024.Day08
{
    public static class Challange1
    {
        private static int Width = 0;
        private static int Height = 0;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var antennas = new Dictionary<char, List<(int X, int Y)>>();
            Height = input.Length;
            Width = input[0].Length;

            //We parese the input grid into simpler list of antennas
            //Or dictionary of lists, with frequency being a key. this allows faster traversal.
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (input[y][x] == '.')
                        continue;

                    //This just creates (and assigns) the list if it doens't exist yet. love this system.
                    if (!antennas.TryGetValue(input[y][x], out var list))
                    {
                        list = new List<(int X, int Y)>(); ;
                        antennas[input[y][x]] = list;
                    }

                    list.Add((x, y));
                }
            }

            //We start creating antinodes by iterating over frequencies.
            var antinodes = new HashSet<(int x, int y)>();
            foreach (var frequency in antennas.Keys)
            {
                //In each frequency we iterate over all antennas in there (twice)
                foreach (var (antenna1X, antenna1Y) in antennas[frequency])
                {
                    foreach (var (antenna2X, antenna2Y) in antennas[frequency])
                    {
                        //And skip if it is same antenna
                        if (antenna1X == antenna2X && antenna1Y == antenna2Y)
                        {
                            continue;
                        }
                        
                        //Then we add antinodes created by them.
                        AddAntinodes(antenna1X, antenna1Y, antenna2X, antenna2Y, ref antinodes);
                    }
                }
            }

            return antinodes.Count;
        }

        public static void AddAntinodes(int antenna1X, int antenna1Y, int antenna2X, int antenna2Y, ref HashSet<(int x, int y)> antinodes)
        {
            //At first we calculate movement vector from antenna1 to antenna2
            var xDist = antenna2X - antenna1X;
            var yDist = antenna2Y - antenna1Y;
            
            //And two positions it will create - before 1st antenna and after 2nd antenna
            var x1 = antenna1X - xDist;
            var y1 = antenna1Y - yDist;

            var x2 = antenna2X + xDist;
            var y2 = antenna2Y + yDist;

            //If they are not out of bounds, they are added to the list
            if (x1 >= 0 && x1 < Width && y1 >= 0 && y1 < Height) antinodes.Add((x1, y1));
            if (x2 >= 0 && x2 < Width && y2 >= 0 && y2 < Height) antinodes.Add((x2, y2));
        }
    }
}