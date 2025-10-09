namespace advent_of_code.Year2019.Day17
{
    public static class Challange1
    {
        private readonly static (int xOffset, int yOffset)[] Offsets = [(1,0), (-1,0),(0,1),(0,-1)];

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
                Split(',').Select(long.Parse).ToArray();

            (var scaffoldMap, _) = Common.ParseData(input);

            var checksum = 0;

            foreach (var (X, Y) in scaffoldMap)
            {
                if (CountNeighbours(scaffoldMap, X, Y) > 2)
                {
                    checksum += X * Y;
                }
            }

            return checksum;
        }

        /// <summary>
        /// Counts the neighbours of cell at x-y
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int CountNeighbours(HashSet<(int, int)> map, int x, int y)
        {
            var neighbours = 0;
            foreach (var (xOffset, yOffset) in Offsets)
            {
                var newX = x + xOffset;
                var newY = y + yOffset;
                if (map.Contains((newX, newY))) neighbours++;
            }

            return neighbours;
        }
    }
}