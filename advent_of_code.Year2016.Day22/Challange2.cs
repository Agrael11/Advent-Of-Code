using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day22
{
    public static class Challange2
    {
        private static Grid<bool> grid = new Grid<bool>(0,0);

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var width = 0;
            var height = 0;
            var smallest = int.MaxValue;
            var largest = int.MinValue;
            (int x, int y) empty = (0, 0);

            for (var i = 2; i < input.Length; i++)
            {
                var data = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var id = data[0].Split('-');
                var x = int.Parse(id[1][1..]);
                var y = int.Parse(id[2][1..]);
                var size = int.Parse(data[1][..^1]);
                var used = int.Parse(data[2][..^1]);
                if (used == 0) empty = (x, y);
                smallest = int.Min(size, smallest);
                largest = int.Max(size, largest);
                width = int.Max(width, x + 1);
                height = int.Max(height, y + 1);
            }

            var average = (smallest + largest) / 2;
            grid = new Grid<bool>(width, height);

            //Only usable if there everything is either 100% transferable or 0% transferable

            for (var i = 2; i < input.Length; i++)
            {
                var data = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var id = data[0].Split('-');
                var x = int.Parse(id[1][1..]);
                var y = int.Parse(id[2][1..]);
                var size = int.Parse(data[1][..^1]);
                grid[x, y] = (size > average);
            }

            //Only works if there are two straightforward paths on top

            var (type, cost) = PathFinding.DoBFS(empty, IsTarget, GetNext);

            return cost + (width-2)*5;
        }

        public static bool IsTarget((int x, int y) position)
        {
            return (position.y == 0 && position.x == grid.Width - 1);
        }

        public static bool IsMe((int x, int y) position)
        {
            return (position.y == 0 && position.x == 0);
        }

        public static IEnumerable<(int x, int y)> GetNext((int x, int y) current)
        {
            for (var xoff = -1; xoff <= 1; xoff++)
            {
                for (var yoff = -1; yoff <= 1; yoff++)
                {
                    if (xoff == 0 && yoff == 0) continue;
                    if (xoff != 0 && yoff != 0) continue;
                    var rx = current.x + xoff;
                    var ry = current.y + yoff;
                    if (rx < 0 || ry < 0 || rx >= grid.Width || ry >= grid.Height) continue;
                    if (grid[rx, ry]) continue;
                    yield return (rx, ry);
                }
            }
        }
    }
}
