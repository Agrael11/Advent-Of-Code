using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day06
{
    public static class Challange2
    {
        private static readonly List<Vector2l> points = new List<Vector2l>();
        private static readonly int Required = 10000;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            points.Clear();
            var maxX = 0;
            var maxY = 0;
            foreach (var line in input)
            {
                var data = line.Split(',');
                var x = int.Parse(data[0].Trim(' '));
                var y = int.Parse(data[1].Trim(' '));
                if (x > maxX) maxX = x;
                if (y > maxY) maxY = y;
                points.Add(new Vector2l(x, y));
            }

            var region = 0;

            for (var x = 0; x <= maxX; x++)
            {
                for (var y = 0; y <= maxY; y++)
                {
                    var closests = GetTotalDistance(x,y);
                    if (closests < Required)
                    {
                        region++;
                    }
                }
            }

            return region;
        }
        private static long GetTotalDistance(int x, int y)
        {
            var total = 0L;
            foreach (var point in points)
            {
                var dist = Vector2l.ManhattanDistance(point, x, y);
                total += dist;
            }
            return total;
        }
    }
}