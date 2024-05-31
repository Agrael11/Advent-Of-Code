using System.Text;

namespace advent_of_code.Year2017.Day21
{
    public static class Common
    {
        private static bool[,] Grid = new bool[3, 3];
        private static int GridLength = 3;
        private static readonly RulesKeeper Rules = new RulesKeeper();

        internal static void ResetGrid()
        {
            GridLength = 3;
            Grid = new bool[3, 3];
            Grid[1, 0] = true;
            Grid[2, 1] = true;
            Grid[0, 2] = true;
            Grid[1, 2] = true;
            Grid[2, 2] = true;
        }

        internal static void ParseRules(string[] rules)
        {
            Rules.Clear();

            foreach (var rule in rules)
            {
                Rules.ParseRule(rule);
            }
        }

        internal static long ExpandGrid()
        {
            var lights = 0L;
            int split;
            if (GridLength % 2 == 0)
            {
                split = 2;
            }
            else
            {
                split = 3;
            }

            var newSplit = split + 1;
            var newLength = (GridLength / split) * (newSplit);
            var newGrid = new bool[newLength, newLength];
            var gridPartLength = GridLength / split;

            for (var y = 0; y < gridPartLength; y++)
            {
                var ny = y * (newSplit);
                for (var x = 0; x < gridPartLength; x++)
                {
                    var nx = x * (newSplit);
                    var exp = Rules[GetString(x, y, split)];
                    lights += AddToGrid(nx, ny, exp, ref newGrid);
                }
            }

            GridLength = newLength;
            Grid = newGrid;

            return lights;
        }

        internal static string GetString(int x, int y, int size)
        {
            var result = new StringBuilder();
            var newX = x * size;
            var newY = y * size;
            for (var yOff = 0; yOff < size; yOff++)
            {
                for (var xOff = 0; xOff < size; xOff++)
                {
                    result.Append(Grid[newX + xOff, newY + yOff] ? '#' : '.');
                }
                if (yOff < size - 1) result.Append('/');
            }

            return result.ToString();
        }

        internal static int AddToGrid(int x, int y, string input, ref bool[,] grid)
        {
            var lights = 0;
            var data = input.Split('/');
            for (var yOff = 0; yOff < data.Length; yOff++)
            {
                var row = data[yOff];
                for (var xOff = 0; xOff < row.Length; xOff++)
                {
                    var isLight = data[yOff][xOff] == '#';
                    if (isLight)
                    {
                        lights++;
                        grid[x + xOff, y + yOff] = true;
                    }
                }
            }

            return lights;
        }
    }
}