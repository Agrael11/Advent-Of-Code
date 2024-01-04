namespace advent_of_code.Year2015.Day09
{
    public static class Challange2
    {
        private static Dictionary<string, Dictionary<string, int>> paths = [];

        static int TravelingSalesman(string currentCity, ref HashSet<string> visited)
        {
            if (visited.Count == paths.Count - 1)
            {
                return 0;
            }

            visited.Add(currentCity);

            int length = int.MinValue;

            foreach (string nextCity in paths.Keys)
            {
                if (visited.Contains(nextCity) || nextCity == currentCity) continue;

                int length2 = paths[currentCity][nextCity] + TravelingSalesman(nextCity, ref visited);
                length = Math.Max(length, length2);
            }

            visited.Remove(currentCity);

            return length;
        }

        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            paths = [];

            foreach (string inputLine in input)
            {
                string[] data = inputLine.Split(' ');

                if (paths.TryGetValue(data[0], out Dictionary<string, int>? value))
                {
                    value.Add(data[2], int.Parse(data[4]));
                }
                else
                {
                    Dictionary<string, int> city = [];
                    city.Add(data[2], int.Parse(data[4]));
                    paths.Add(data[0], city);
                }

                if (paths.TryGetValue(data[2], out value))
                {
                    value.Add(data[0], int.Parse(data[4]));
                }
                else
                {
                    Dictionary<string, int> city = [];
                    city.Add(data[0], int.Parse(data[4]));
                    paths.Add(data[2], city);
                }
            }


            int shortest = int.MinValue;

            foreach (string startPoint in paths.Keys)
            {
                HashSet<string> visited = [];
                shortest = Math.Max(shortest, TravelingSalesman(startPoint, ref visited));
            }

            return shortest;
        }
    }
}
