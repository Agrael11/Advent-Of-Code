using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day22
{
    public static class Challange1
    {
        private static DynamicGrid<DataNode> grid = new DynamicGrid<DataNode>(0, 0);

        public static int DoChallange(string inputData)
        {

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            for (var i = 2; i < input.Length; i++)
            {
                var data = input[i].Split(' ',StringSplitOptions.RemoveEmptyEntries);
                var id = data[0].Split('-');
                var x = int.Parse(id[1][1..]);
                var y = int.Parse(id[2][1..]);
                var size = int.Parse(data[1][..^1]);
                var used = int.Parse(data[2][..^1]);
                var node = new DataNode(x, y, size, used);
                if (grid.Width < x + 1 || grid.Height < y + 1) grid.Expand(x + 1, y + 1);
                grid[x, y] = node;
            }

            var pairs = 0;

            foreach (var node1 in grid)
            {
                foreach (var node2 in grid)
                {
                    if (node1.X == node2.X && node1.Y == node2.Y) continue;
                    pairs += Fits(node1, node2) ? 1 : 0;
                }
            }

            grid = new DynamicGrid<DataNode>(0, 0);

            return pairs;
        }

        private static bool Fits(DataNode? node1, DataNode? node2)
        {
            if (node1 is null || node2 is null) return false;

            return (node1.Used > 0 && node2.Available >= node1.Used);
        }
    }
}