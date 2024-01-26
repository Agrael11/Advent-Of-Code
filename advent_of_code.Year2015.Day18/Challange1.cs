using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day18
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var lightsOn = 0;

            var width = input[0].Length;
            var height = input.Length;
            var grid = new Grid<bool>(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = input[y][x] == '#';
                }
            }

            for (var step = 0; step < 100; step++)
            {
                DoStep(ref grid, ref lightsOn);
            }

            return lightsOn;
        }

        public static void DoStep(ref Grid<bool> grid, ref int lights)
        {
            lights = 0;
            var newGrid = new Grid<bool>(grid.Width, grid.Height);
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var on = CountAround(grid, x, y);
                    if (grid[x, y])
                    {
                        if (on < 2 || on > 3)
                        {
                            newGrid[x, y] = false;
                        }
                        else
                        {
                            newGrid[x, y] = true;
                            lights++;
                        }
                    }
                    else
                    {
                        if (on == 3)
                        {
                            newGrid[x, y] = true;
                            lights++;
                        }
                        else
                        {
                            newGrid[x, y] = false;
                        }
                    }
                }
            }
            grid = newGrid;
        }

        public static int CountAround(Grid<bool> grid, int x, int y)
        {
            var total = 0;

            for (var _y = -1; _y <= 1; _y++)
            {
                for (var _x = -1; _x <= 1; _x++)
                {
                    if (_y == 0 && _x == 0) continue;

                    if (grid[x + _x, y + _y]) total++;
                }
            }

            return total;
        }
    }
}