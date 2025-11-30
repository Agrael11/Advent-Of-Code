namespace advent_of_code.Year2019.Day24
{
    public static class Challange1
    {
        public static ulong DoChallange(string inputData)
        {
            //inputData = "....#\r\n#..#.\r\n#..##\r\n..#..\r\n#....";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            var grid = Grid.Parse(input);
            var layouts = new HashSet<ulong>();
            
            while (true)
            {
                if (!layouts.Add(grid.GridData))
                {
                    return grid.GridData;
                }
                RunGrid(grid);
            }
        }

        private static void RunGrid(Grid grid)
        {
            ulong newGrid = 0;
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var count = CountAround(grid, x, y);
                    if (grid[x, y])
                    {
                        grid.SetFor(ref newGrid, x, y, count == 1);
                    }
                    else
                    {
                        grid.SetFor(ref newGrid, x, y, count == 1 || count == 2);
                    }
                }
            }
            grid.GridData = newGrid;
        }

        private static int CountAround(Grid grid, int x, int y)
        {
            var count = 0;
            if (x > 0 && grid[x - 1, y]) count++;
            if (x < grid.Width - 1 && grid[x + 1, y]) count++;
            if (y > 0 && grid[x, y - 1]) count++;
            if (y < grid.Height- 1 && grid[x, y + 1]) count++;
            return count;
        }
    }
}