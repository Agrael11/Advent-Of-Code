using System.Runtime.InteropServices;
using System.Text;
using Visualizers;

namespace advent_of_code.Year2019.Day24
{
    public static class Challange2
    {
        private static readonly (int X, int Y)[] directions = [
            (-1, 0),
            (+1, 0),
            (0, -1),
            (0, +1) ];

        private static readonly int TargetMinutes = 200;

        private static readonly Dictionary<int, Grid> layers = new Dictionary<int, Grid>();
        private static int centerX = 0;
        private static int centerY = 0;
        private static int width = 0;
        private static int height = 0;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var grid = Grid.Parse(input);
            width = grid.Width;
            height = grid.Height;
            centerX = width / 2;
            centerY = height / 2;
            
            layers.Clear();
            layers.Add(0, grid);

            var data = "";
            if (AOConsole.Enabled)
            {
                data = DrawLayersIntoString(0);
            }

            for (var i = 0; i < TargetMinutes; i++)
            {
                SingleRun();
                if (AOConsole.Enabled)
                {
                    data += DrawLayersIntoString(i + 1);
                }
            }

            var bugCount = layers.Sum(t => t.Value.CountBugs());

            if (AOConsole.Enabled)
            {
                AOConsole.WriteLine(data + "BugCount: " + bugCount);
            }

            return bugCount;
        }

        public static string DrawLayersIntoString(int minute)
        {
            var builder = new StringBuilder();
            builder.AppendLine("MINUTE " + minute + "");
            builder.AppendLine("------------------------------");
            foreach ((var layer, var grid) in layers.OrderBy(t=>t.Key))
            {
                builder.AppendLine("Depth " + layer + ":");
                builder.AppendLine(grid.Draw());
                builder.AppendLine("");
            }
            builder.AppendLine("------------------------------");
            return builder.ToString();
        }

        public static void SingleRun()
        {
            var newMap = new Dictionary<int, ulong>();
            var existingLayers = layers.Keys.ToList();
            foreach (var layer in existingLayers)
            {
                newMap[layer] = DoLayer(layer);
                newMap[layer + 1] = DoLayer(layer + 1);
                newMap[layer - 1] = DoLayer(layer - 1);
            }
            foreach ((var layer, var layerData) in newMap)
            {
                if (layerData == 0 && layers.ContainsKey(layer))
                {
                    layers.Remove(layer);
                    continue;
                }
                var grid = GetGridAtLevel(layer);
                grid.GridData = layerData;
            }
        }

        private static ulong DoLayer(int layer)
        {
            var grid = GetGridAtLevel(layer);
            ulong newLayer = 0;
            for (var y = 0; y < width; y++)
            {
                for (var x= 0; x <  height; x++)
                {
                    if (x == centerX && y == centerY) continue;
                    var rule = GetByRules(grid[x, y], CountAround(layer, x, y));
                    grid.SetFor(ref newLayer, x, y, rule);
                }
            }
            return newLayer;
        }

        private static bool GetByRules(bool current, int count)
        {
            if (current) return (count == 1);
            else return (count == 1 || count == 2);
        }

        private static int CountAround(int layer, int x, int y)
        {
            var count = 0;
            foreach ((var offsetX, var offsetY) in directions)
            {
                var nextX = x + offsetX;
                var nextY = y + offsetY;
                count += GetAt(layer, nextX, nextY, offsetX, offsetY);
            }
            return count;
        }

        private static int GetAt(int layer, int nextX, int nextY, int dirX, int dirY)
        {
            if (nextX == -1 || nextY == -1 || nextX == width || nextY == height)
            {
                if (!TryGetGridAtLevel(layer - 1, out var nextGrid)) return 0;
                var newX = centerX + dirX;
                var newY = centerY + dirY;
                return nextGrid[newX, newY]?1:0;
            }
            if (nextX == centerX && nextY == centerY)
            {
                if (!TryGetGridAtLevel(layer + 1, out var nextGrid)) return 0;
                var count = 0;
                if (dirX == 0)
                {
                    //Horizontal - we moved vertically
                    var newY = centerY - (dirY * 2);
                    for (var newX = 0; newX < width; newX++)
                    {
                        count += (nextGrid[newX, newY])?1:0;
                    }
                }
                else if (dirY == 0)
                {
                    //Vertical - we moved horizontally
                    var newX = centerX - (dirX * 2);
                    for (var newY = 0; newY < height; newY++)
                    {
                        count += (nextGrid[newX, newY]) ? 1 : 0;
                    }
                }
                return count;
            }
            return GetGridAtLevel(layer)[nextX, nextY]?1:0;
        }

        private static bool TryGetGridAtLevel(int level, out Grid grid)
        {
            if (layers.TryGetValue(level, out var o_grid) && o_grid is not null)
            {
                grid = o_grid;
                return true;
            }
            grid = new Grid(width, height);
            return false;
        }

        private static Grid GetGridAtLevel(int level)
        {
            if (TryGetGridAtLevel(level, out var grid)) return grid;
            layers.Add(level, grid);
            return grid;    
        }
    }
}