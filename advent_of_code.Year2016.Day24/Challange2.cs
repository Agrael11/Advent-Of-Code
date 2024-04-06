using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day24
{
    public static class Challange2
    {
        private static Grid<int> grid = new Grid<int>(0, 0);
        private static int currentTarget = 0;
        private static readonly Dictionary<int, Node> points = new Dictionary<int, Node>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            grid = new Grid<int>(input[0].Length, input.Length);

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (int.TryParse(input[y][x].ToString(), out var num))
                    {
                        grid[x, y] = num;
                        points.Add(num, new Node(num, x, y));
                    }
                    else if (input[y][x] == '.')
                    {
                        grid[x, y] = -1;
                    }
                    else if (input[y][x] == '#')
                    {
                        grid[x, y] = -2;
                    }
                }
            }


            for (var start = 0; start < points.Count; start++)
            {
                for (var end = start + 1; end < points.Count; end++)
                {
                    currentTarget = end;
                    var (type, length) = PathFinding.DoBFS(points[start].Position, IsEnd, GetNext);
                    if (length != -1)
                    {
                        points[start].AddConnection(end, length);
                        points[end].AddConnection(start, length);
                    }
                }
            }

            var cities = Enumerable.Range(1, points.Count - 1);

            var minDistance = int.MaxValue;

            foreach (var permutation in cities.YieldPermutate())
            {
                minDistance = int.Min(CalculateDistance(permutation), minDistance);
            }

            points.Clear();
            return minDistance;
        }

        private static int CalculateDistance(List<int> permutation)
        {
            var distance = 0;
            for (var i = 0; i < permutation.Count - 1; i++)
            {
                distance += points[permutation[i]].Targets[permutation[i + 1]];
            }
            return distance + points[permutation[0]].Targets[0] + points[permutation[^1]].Targets[0];
        }

        private static bool IsEnd((int x, int y) current)
        {
            return grid[current.x, current.y] == currentTarget;
        }

        private static IEnumerable<(int x, int y)> GetNext((int x, int y) current)
        {
            for (var ty = -1; ty <= 1; ty++)
            {
                for (var tx = -1; tx <= 1; tx++)
                {
                    if (tx == 0 && ty == 0) continue;
                    if (tx != 0 && ty != 0) continue;

                    var x = current.x + tx;
                    var y = current.y + ty;
                    if (x < 0 || x >= grid.Width || y < 0 || y >= grid.Height) continue;

                    if (grid[x, y] == -2) continue;
                    yield return (x, y);
                }
            }
        }
    }
}