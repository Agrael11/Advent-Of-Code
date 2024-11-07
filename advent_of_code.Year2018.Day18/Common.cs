namespace advent_of_code.Year2018.Day18
{
    internal class Common
    {
        internal static void ParseOneMinute(Grid grid)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    switch (grid[x, y])
                    {
                        case Grid.State.Ground:
                            grid[x, y] = (grid.CountAround(Grid.State.Trees, x, y) >= 3) ? Grid.State.Trees : Grid.State.Ground;
                            break;
                        case Grid.State.Trees:
                            grid[x, y] = (grid.CountAround(Grid.State.Lumberyard, x, y) >= 3) ? Grid.State.Lumberyard : Grid.State.Trees;
                            break;
                        case Grid.State.Lumberyard:
                            var requirement = (grid.CountAround(Grid.State.Lumberyard, x, y) >= 1)
                                && (grid.CountAround(Grid.State.Trees, x, y) >= 1);
                            grid[x, y] = requirement ? Grid.State.Lumberyard : Grid.State.Ground;
                            break;
                    }
                }
            }
            grid.Apply();
        }
    }
}
