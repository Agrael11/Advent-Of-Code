using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day03
{
    internal class Common
    {
        public static List<Line> GenerateLines(string[] inputs)
        {
            var lines = new List<Line>();
            var point = new Vector2l(0, 0);
            foreach (var input in inputs)
            {
                var inputDir = input.ToUpper()[0];
                var inputLength = int.Parse(input[1..]);
                Line line;
                switch (inputDir)
                {
                    case 'R':
                        line = new Line(new Vector2l(point.X, point.Y), inputLength, Line.LineOrientation.Horizontal);
                        point = new Vector2l(line.X2, point.Y);
                        break;
                    case 'L':
                        line = new Line(new Vector2l(point.X, point.Y), inputLength * -1, Line.LineOrientation.Horizontal);
                        point = new Vector2l(line.X1, point.Y);
                        break;
                    case 'D':
                        line = new Line(new Vector2l(point.X, point.Y), inputLength, Line.LineOrientation.Vertical);
                        point = new Vector2l(point.X, line.Y2);
                        break;
                    case 'U':
                        line = new Line(new Vector2l(point.X, point.Y), inputLength * -1, Line.LineOrientation.Vertical);
                        point = new Vector2l(point.X, line.Y1);
                        break;
                    default:
                        throw new Exception("Unexpected Character");
                }
                ;
                lines.Add(line);
            }
            return lines;
        }
    }
}
