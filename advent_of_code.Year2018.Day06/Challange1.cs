using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day06
{
    public static class Challange1
    {
        private static readonly List<Vector2l> points = new List<Vector2l>();
        
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

            var counts = new Dictionary<Vector2l, int>();
            var largest = 0;

            for (var x = 0; x <= maxX; x++)
            {
                for (var y = 0; y <= maxY; y++)
                {
                    var closest = GetClosest(x,y);
                    if (closest is not null)
                    {
                        if (x == 0 || y == 0 || x == maxX || y == maxY)
                        {
                            counts[closest] = int.MinValue;
                            continue;
                        }

                        counts.TryGetValue(closest, out var count);
                        counts[closest] = ++count;
                        largest = int.Max(largest, count);
                    }
                }
            }

            return largest;
        }

        private static Vector2l? GetClosest(int x, int y)
        {
            var lowestDistance = long.MaxValue;
            Vector2l? lowestItem = null;
            foreach (var point in points)
            {
                var dist = Vector2l.ManhattanDistance(point, x, y);
                if (dist < lowestDistance)
                {
                    lowestDistance = dist;
                    lowestItem = point;
                }
                else if (dist == lowestDistance)
                {
                    lowestItem = null;
                }
            }
            return lowestItem;
        }
    }
}