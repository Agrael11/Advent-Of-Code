using System.Runtime.ExceptionServices;

namespace advent_of_code.Year2015.Day09
{
    public static class Challange1
    {
        private static Dictionary<string, Dictionary<string, int>> paths = [];

        static int TravelingSalesman(string currentCity, ref HashSet<string> visited)
        {
            if (visited.Count == paths.Count-1)
            {
                return 0;
            }

            visited.Add(currentCity);

            int length = int.MaxValue;

            foreach (string nextCity in paths.Keys)
            {
                if (visited.Contains(nextCity) || nextCity == currentCity) continue;

                length = Math.Min(length, paths[currentCity][nextCity] + TravelingSalesman(nextCity, ref visited));
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

            HashSet<string> visited = [];
            int shortest = int.MaxValue;

            foreach (string startPoint in paths.Keys)
            {
                visited.Clear();
                shortest = Math.Min(shortest, TravelingSalesman(startPoint, ref visited));
            }

            return shortest;
        }
    }
}