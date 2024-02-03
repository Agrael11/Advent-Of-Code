using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day24
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var packages = new List<int>();
            foreach (var line in input)
            {
                packages.Add(int.Parse(line));
            }

            var target = packages.Sum() / 4;

            var foundSize = -1;
            var minProduct = long.MaxValue;

            foreach (var item in packages.YieldCombinations())
            {
                var size = item.Count;
                if (foundSize != -1 && size > foundSize)
                    break;

                if (item.Sum() == target)
                {
                    foundSize = size;
                    minProduct = Math.Min(minProduct, item.Aggregate((long)1, (acc, val) => acc * val));
                }
            }

            return minProduct;
        }
    }
}