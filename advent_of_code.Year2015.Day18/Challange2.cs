﻿using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day18
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            int lightsOn = 0;

            int width = input[0].Length;
            int height = input.Length;
            Grid<bool> grid = new(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = input[y][x] == '#';
                }
            }
            SetCornersOn(ref grid);

            for (int step = 0; step < 100; step++)
            {
                DoStep(ref grid, ref lightsOn);
            }

            return lightsOn;
        }
        public static bool IsCorner(Grid<bool> grid, int x, int y)
        {
            return (x == 0 && (y == 0 || y == grid.Height - 1)) || (x == grid.Width - 1 && (y == 0 || y == grid.Height - 1));
        }

        public static void SetCornersOn(ref Grid<bool> grid)
        {
            grid[0, 0] = true;
            grid[0, grid.Height - 1] = true;
            grid[grid.Width - 1, 0] = true;
            grid[grid.Width - 1, grid.Height - 1] = true;
        }

        public static void DoStep(ref Grid<bool> grid, ref int lights)
        {
            lights = 0;
            Grid<bool> newGrid = new(grid.Width, grid.Height);
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    int on = CountAround(grid, x, y);
                    if (grid[x, y])
                    {
                        if (on == 2 || on == 3 || IsCorner(grid, x, y))
                        {
                            newGrid[x, y] = true;
                            lights++;
                        }
                        else
                        {
                            newGrid[x, y] = false;
                        }
                    }
                    else
                    {
                        if (on == 3 || IsCorner(grid, x, y))
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
            int total = 0;

            for (int _y = -1; _y <= 1; _y++)
            {
                for (int _x = -1; _x <= 1; _x++)
                {
                    if (_y == 0 && _x == 0) continue;

                    if (grid[x + _x, y + _y]) total++;
                }
            }

            return total;
        }
    }
}