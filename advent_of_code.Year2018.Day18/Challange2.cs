namespace advent_of_code.Year2018.Day18
{
    public static class Challange2
    {
        private static readonly int Minutes = 1_000_000_000;

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

            var hashes = new HashSet<int>();
            var orderedHashes = new List<int>();
            var orderedResults = new List<int>();
            var minute = 0;
            var lastHash = -1;

            for (; minute < Minutes; minute++)
            {
                Common.ParseOneMinute(grid);
                lastHash = grid.GetGridHash();
                if (hashes.Add(lastHash))
                {
                    var result = grid.Count(Grid.State.Trees) * grid.Count(Grid.State.Lumberyard);
                    orderedHashes.Add(lastHash);
                    orderedResults.Add(result);
                }
                else
                {
                    break;
                }
            }
            var target = Minutes - 1;
            var loopStart = orderedHashes.IndexOf(lastHash);
            var loopLength = minute - loopStart;
            target -= loopStart;
            
            return orderedResults[loopStart + (target%loopLength)];
        }
    }
}