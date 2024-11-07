namespace advent_of_code.Year2018.Day18
{
    public static class Challange1
    {
        private static readonly int Minutes = 10;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var grid = new Grid(input.Length, input[0].Length);
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    grid[x, y] = input[y][x] switch
                    {
                        '#' => Grid.State.Lumberyard,
                        '|' => Grid.State.Trees,
                        _ => Grid.State.Ground,
                    };
                }
            }
            grid.Apply();

            for (var minute = 0; minute < Minutes; minute++)
            {
                Common.ParseOneMinute(grid);   
            }

            return grid.Count(Grid.State.Trees) * grid.Count(Grid.State.Lumberyard);
        }

        
    }
}