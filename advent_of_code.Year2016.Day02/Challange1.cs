namespace advent_of_code.Year2016.Day02
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var currentX = 1;
            var currentY = 1;
            var code = 0;

            foreach (var line in input)
            {
                code *= 10;
                foreach (var c in line)
                {
                    switch (c)
                    {
                        case 'U': currentY--; break;
                        case 'D': currentY++; break;
                        case 'L': currentX--; break;
                        case 'R': currentX++; break;
                    }
                    currentX = Math.Clamp(currentX, 0, 2);
                    currentY = Math.Clamp(currentY, 0, 2);
                }
                code += 1 + currentY * 3 + currentX;
            }

            return code;
        }
    }
}