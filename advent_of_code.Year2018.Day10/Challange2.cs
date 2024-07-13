using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day10
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var points = new List<Point>();


            foreach (var line in input)
            {
                points.Add(new Point(line));
            }

            var lowestVariationTime = HeightCalculator.GetLowestHeightTime(points, 0, 5000);
            
            return lowestVariationTime;
        }
    }
}