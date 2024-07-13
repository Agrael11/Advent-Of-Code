using advent_of_code.Helpers;
using System.Drawing;

namespace advent_of_code.Year2018.Day10
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var points = new List<Point>();


            foreach (var line in input)
            {
                points.Add(new Point(line));
            }

            var lowestVariationTime = HeightCalculator.GetLowestHeightTime(points, 0, 5000);
            points = Point.GetAfter(points, lowestVariationTime);

            var left = points.Min(p => p.Position.X);
            var top = points.Min(p => p.Position.Y);
            var width = points.Max(p => p.Position.X) - left + 1;
            var height = points.Max(p => p.Position.Y)-top + 1;
            var results = new Grid<bool>((int)width, (int)height);

            foreach (var point in points)
            {
                results[(int)(point.Position.X - left), (int)(point.Position.Y - top)] = true;
            }

            var result = Helpers.AsciiArtReader.Reader.ReadText(results, 2, Helpers.AsciiArtReader.Reader.FontTypes.Type_B);
            return $"{result.Text}{(result.FoundAll ? "" : " (contains unknown characters)")}";
        }
    }
}