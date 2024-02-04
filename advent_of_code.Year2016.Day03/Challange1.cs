namespace advent_of_code.Year2016.Day03
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var validTrianles = 0;
            foreach (var line in input)
            {
                var triangleData = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                validTrianles += TriangleValid(int.Parse(triangleData[0]), int.Parse(triangleData[1]), int.Parse(triangleData[2])) ? 1 : 0;
            }

            return validTrianles;
        }

        private static bool TriangleValid(int sideA, int sideB, int sideC)
        {
            return (sideA + sideB > sideC) && (sideA + sideC > sideB) && (sideB + sideC > sideA);
        }
    }
}