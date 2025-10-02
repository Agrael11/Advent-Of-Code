using System.Text;

namespace advent_of_code.Year2019.Day11
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",").Select(long.Parse).ToArray();

            var robot = Common.RunNewRobot(input, Robot.Color.White);

            var paintedPanelsDict = robot.PaintedPanels;

            var str = RenderRegistrationIdentifier(paintedPanelsDict);
            Visualizers.AOConsole.Write(str);

            var result = Helpers.AsciiArtReader.Reader.ReadText(str, 1, Helpers.AsciiArtReader.Reader.FontTypes.Type_A);

            return result.Text;
        }

        private static string RenderRegistrationIdentifier(HashSet<(int X, int Y)> paintedPanelsDict)
        {
            var minX = paintedPanelsDict.Min(p => p.X);
            var minY = paintedPanelsDict.Min(p => p.Y);
            var maxX = paintedPanelsDict.Max(p => p.X);
            var maxY = paintedPanelsDict.Max(p => p.Y);
            var width = maxX - minX + 1;
            var height = maxY - minY + 1;
            var builder = new StringBuilder();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (paintedPanelsDict.Contains ((x + minX, y + minY)))
                    {
                        builder.Append('#');
                        continue;
                    }
                    else
                    {
                        builder.Append('.');
                    }
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}