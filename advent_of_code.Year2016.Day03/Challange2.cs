namespace advent_of_code.Year2016.Day03
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var validTrianles = 0;
            for (var lineIndex = 0; lineIndex < input.Length; lineIndex += 3)
            {
                var line1Split = input[lineIndex].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var line2Split = input[lineIndex + 1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var line3Split = input[lineIndex + 2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                validTrianles += TriangleValid(int.Parse(line1Split[0]), int.Parse(line2Split[0]), int.Parse(line3Split[0])) ? 1 : 0;
                validTrianles += TriangleValid(int.Parse(line1Split[1]), int.Parse(line2Split[1]), int.Parse(line3Split[1])) ? 1 : 0;
                validTrianles += TriangleValid(int.Parse(line1Split[2]), int.Parse(line2Split[2]), int.Parse(line3Split[2])) ? 1 : 0;   
            }
            return validTrianles;
        }

        private static bool TriangleValid(int sideA, int sideB, int sideC)
        {
            return (sideA + sideB > sideC) && (sideA + sideC > sideB) && (sideB + sideC > sideA);
        }
    }
}