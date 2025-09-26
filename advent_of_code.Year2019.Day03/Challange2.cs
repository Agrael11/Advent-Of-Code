using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day03
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t => t.Split(',')).ToArray();

            var path1 = Common.GenerateLines(input[0]);
            var path2 = Common.GenerateLines(input[1]);

            var closest = long.MaxValue;

            var points = Line.GetIntersectPoints(path1, path2);

            foreach (var intersectPoint in Line.GetIntersectPoints(path1, path2))
            {
                if (intersectPoint.X == 0 && intersectPoint.Y == 0) continue;
                var distance = GetNumberOfStepsToReach(path1, intersectPoint) + GetNumberOfStepsToReach(path2, intersectPoint);
                if (distance < closest) { closest = distance; }
            }

            return closest;
        }

        private static long GetNumberOfStepsToReach(List<Line> path, Vector2l point)
        {
            var totalDistance = 0L;
            foreach (var line in path)
            {
                var distance = Line.GetDistance(line, point);
                if (distance <= 0) totalDistance += line.Length;
                else return totalDistance + distance;
            }
            return totalDistance;
        }
    }
}