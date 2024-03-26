namespace advent_of_code.Year2015.Day09
{
    public static class Challange2
    {
        private static readonly Dictionary<string, Dictionary<string, int>> paths =
            new Dictionary<string, Dictionary<string, int>>();

        private static int TravelingSalesman(string currentCity, ref HashSet<string> visited)
        {
            if (visited.Count == paths.Count - 1)
            {
                return 0;
            }

            visited.Add(currentCity);

            var length = int.MinValue;

            foreach (var nextCity in paths.Keys)
            {
                if (visited.Contains(nextCity) || nextCity == currentCity) continue;

                var length2 = paths[currentCity][nextCity] + TravelingSalesman(nextCity, ref visited);
                length = Math.Max(length, length2);
            }

            visited.Remove(currentCity);

            return length;
        }

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (var inputLine in input)
            {
                var data = inputLine.Split(' ');

                if (paths.TryGetValue(data[0], out var value))
                {
                    value.Add(data[2], int.Parse(data[4]));
                }
                else
                {
                    var city = new Dictionary<string, int>();
                    city.Add(data[2], int.Parse(data[4]));
                    paths.Add(data[0], city);
                }

                if (paths.TryGetValue(data[2], out value))
                {
                    value.Add(data[0], int.Parse(data[4]));
                }
                else
                {
                    var city = new Dictionary<string, int>();
                    city.Add(data[0], int.Parse(data[4]));
                    paths.Add(data[2], city);
                }
            }


            var shortest = int.MinValue;

            foreach (var startPoint in paths.Keys)
            {
                var visited = new HashSet<string>();
                shortest = Math.Max(shortest, TravelingSalesman(startPoint, ref visited));
            }

            paths.Clear();

            return shortest;
        }
    }
}
