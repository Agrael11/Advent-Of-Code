using System.Collections.Concurrent;

namespace advent_of_code.Year2017.Day22
{
    public static class Challange2
    {
        private enum NodeState { Clean, Weakened, Infected, Flagged}
        private static readonly Dictionary<(int x, int y), NodeState> grid = new Dictionary<(int x, int y), NodeState> ();
        private static readonly int Bursts = 10000000;

        public static int DoChallange(string inputData)
        {
            grid.Clear();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            (int x, int y) current = (input[0].Length / 2, input.Length / 2);
            var currentDirection = 0;

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#') grid[(x, y)] = NodeState.Infected;
                }
            }

            var infections = 0;
            for (var i = 0; i < Bursts; i++)
            {
                grid.TryGetValue(current, out var state);
                switch (state)
                {
                    default:
                    case NodeState.Clean:
                        currentDirection = ((currentDirection + 3) % 4);
                        grid[current] = NodeState.Weakened;
                        break;
                    case NodeState.Weakened:
                        infections++;
                        grid[current] = NodeState.Infected;
                        break;
                    case NodeState.Infected:
                        currentDirection = ((currentDirection + 1) % 4); 
                        grid[current] = NodeState.Flagged;
                        break;
                    case NodeState.Flagged:
                        currentDirection = ((currentDirection + 2) % 4);
                        grid[current] = NodeState.Clean;
                        break;
                }

                switch (currentDirection)
                {
                    case 0:
                        current.y--;
                        break;
                    case 1:
                        current.x++;
                        break;
                    case 2:
                        current.y++;
                        break;
                    case 3:
                        current.x--;
                        break;
                }
            }

            return infections;
        }
    }
}