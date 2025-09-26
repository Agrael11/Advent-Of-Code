namespace advent_of_code.Year2019.Day03
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t=>t.Split(',')).ToArray();

            var path1 = Common.GenerateLines(input[0]);
            var path2 = Common.GenerateLines(input[1]);

            var closest = long.MaxValue;

            foreach (var intersectPoint in Line.GetIntersectPoints(path1, path2))
            {
                var distance = Math.Abs(intersectPoint.X) + Math.Abs(intersectPoint.Y);
                if (distance != 0 && distance < closest) { closest = distance; }
            }

            return closest;
        }
    }
}