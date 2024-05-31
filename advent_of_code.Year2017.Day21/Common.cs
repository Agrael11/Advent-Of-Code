namespace advent_of_code.Year2017.Day21
{
    public static class Common
    {
        private static bool[,] Grid = new bool[3, 3];
        private static int GridLength = 3;
        private static readonly Dictionary<string, string> Rules = new Dictionary<string, string>();

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
                var ruleData = rule.Split(" => ");
                var rotRule = Rotate90(ruleData[0]);
                Rules.TryAdd(ruleData[0], ruleData[1]);
                Rules.TryAdd(FlipHorizontal(ruleData[0]), ruleData[1]);
                Rules.TryAdd(FlipVertical(ruleData[0]), ruleData[1]);
                Rules.TryAdd(FlipVertical(FlipHorizontal(ruleData[0])), ruleData[1]);
                Rules.TryAdd(rotRule, ruleData[1]);
                Rules.TryAdd(FlipHorizontal(rotRule), ruleData[1]);
                Rules.TryAdd(FlipVertical(rotRule), ruleData[1]);
                Rules.TryAdd(FlipVertical(FlipHorizontal(rotRule)), ruleData[1]);
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

            var newLength = (GridLength / split) * (split + 1);
            var newGrid = new bool[newLength, newLength];

            for (var y = 0; y < GridLength / split; y++)
            {
                var ny = y * (split + 1);
                for (var x = 0; x < GridLength / split; x++)
                {
                    var nx = x * (split + 1);
                    var exp = Rules[GetString(x, y, split)];
                    lights += AddToGrid(nx, ny, exp, ref newGrid);
                }
            }

            GridLength = newLength;
            Grid = newGrid;

            return lights;
        }

        private static string Rotate90(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var x = split[0].Length - 1; x >= 0; x--)
            {
                for (var y = 0; y < split.Length; y++)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static string FlipHorizontal(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var y = 0; y < split.Length; y++)
            {
                for (var x = split[0].Length - 1; x >= 0; x--)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static string FlipVertical(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var y = split.Length - 1; y >= 0; y--)
            {
                for (var x = 0; x < split[0].Length; x++)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static string GetString(int x, int y, int size)
        {
            var output = "";
            var newX = x * size;
            var newY = y * size;
            for (var yOff = 0; yOff < size; yOff++)
            {
                for (var xOff = 0; xOff < size; xOff++)
                {
                    output += Grid[newX + xOff, newY + yOff] ? "#" : ".";
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static int AddToGrid(int x, int y, string input, ref bool[,] grid)
        {
            var lights = 0;
            var data = input.Split('/');
            for (var yOff = 0; yOff < data.Length; yOff++)
            {
                for (var xOff = 0; xOff < data[yOff].Length; xOff++)
                {
                    lights += (data[yOff][xOff] == '#') ? 1 : 0;
                    grid[x + xOff, y + yOff] = data[yOff][xOff] == '#';
                }
            }

            return lights;
        }
    }
}