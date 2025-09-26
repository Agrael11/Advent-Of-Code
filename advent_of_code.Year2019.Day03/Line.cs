using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day03
{
    internal class Line
    {
        public enum LineOrientation { Horizontal, Vertical }
        public Vector2l StartPoint { get; set; }
        public long Length { get; set; }
        public bool Backwards { get; set; } = false;
        public LineOrientation Orientation { get; set; }

        public long X1 => StartPoint.X;
        public long X2 => StartPoint.X + ((Orientation == LineOrientation.Horizontal) ? Length : 0);
        public long Y1 => StartPoint.Y;
        public long Y2 => StartPoint.Y + ((Orientation == LineOrientation.Vertical) ? Length : 0);

        public Line(Vector2l startPoint, long length, LineOrientation orientation)
        {
            StartPoint = startPoint;
            Length = length;
            Orientation = orientation;
            if (length < 0)
            {
                Length = Math.Abs(length);
                if (orientation == LineOrientation.Horizontal) StartPoint.X -= Length;
                else StartPoint.Y -= Length;
                Backwards = true;
            }
        }

        public static long GetDistance(Line line, Vector2l point)
        {
            if (line.Orientation == LineOrientation.Horizontal)
            {
                if (point.Y == line.Y1)
                {
                    return GetDistanceHorizontal(line, point.X);
                }
            }
            else
            {
                if (point.X == line.X1)
                {
                    return GetDistanceVertical(line, point.Y);
                }
            }
            return -1;
        }

        private static long GetDistanceHorizontal(Line line, long point)
        {
            if (point < line.X1 || point > line.X2) return -1;
            if (line.Backwards) return line.X2 - point;
            return point - line.X1;
        }

        private static long GetDistanceVertical(Line line, long point)
        {
            if (point < line.Y1 || point > line.Y2) return -1;
            if (line.Backwards) return line.Y2 - point;
            return point - line.Y1;
        }

        public static List<Vector2l> GetIntersectPoints(List<Line> lines1, List<Line> lines2)
        {
            var IntersectPoints = new List<Vector2l>();
            foreach (var line1 in lines1)
            {
                IntersectPoints.AddRange(GetIntersectPoints(line1, lines2));
            }
            IntersectPoints = IntersectPoints.Distinct().ToList();
            return IntersectPoints;
        }
        
        public static List<Vector2l> GetIntersectPoints(Line line1, List<Line> otherLines)
        {
            var IntersectPoints = new List<Vector2l>();
            foreach (var line2 in otherLines)
            {
                IntersectPoints.AddRange(GetIntersectPoints(line1, line2));
            }
            IntersectPoints = IntersectPoints.Distinct().ToList();
            return IntersectPoints;
        }

        public static List<Vector2l> GetIntersectPoints(Line line1, Line line2)
        {
            if (line1.Orientation != line2.Orientation)
            {
                if (line1.Orientation == LineOrientation.Horizontal) 
                    return GetIntersectPointsPerpendicular(line1, line2);
                else
                    return GetIntersectPointsPerpendicular(line2, line1);
            }
            return GetIntersectPointsParallel(line1, line2); //Not sure if this is ever used
        }

        private static List<Vector2l> GetIntersectPointsParallel(Line line1, Line line2)
        {
            var IntersectPoints = new List<Vector2l>();
            if (line1.Orientation == LineOrientation.Horizontal && line1.Y1 == line2.Y1 && 
                line1.X1 <= line2.X2 && line1.X2>= line2.X1)
            {
                for (var x = line1.X1; x <= line1.X2; x++)
                {
                    if (x >= line2.X1 && x <= line2.X2)
                        IntersectPoints.Add(new Vector2l(x, line1.Y1));
                }
            }
            else if (line1.Orientation == LineOrientation.Vertical && line1.X1 == line2.X1 &&
                line1.Y1 <= line2.Y2 && line1.Y2 >= line2.Y1)
            {
                for (var y = line1.Y1; y <= line1.Y2; y++)
                {
                    if (y >= line2.Y1 && y <= line2.Y2)
                        IntersectPoints.Add(new Vector2l(line1.X1, y));
                }
            }

            return IntersectPoints;
        }

        private static List<Vector2l> GetIntersectPointsPerpendicular(Line line1, Line line2)
        {
            var IntersectPoints = new List<Vector2l>();

            if (line2.X1 >= line1.X1 && line2.X1 <= line1.X2 &&
                line1.Y1 >= line2.Y1 && line1.Y1 <= line2.Y2)
            {
                IntersectPoints.Add(new Vector2l(line2.X1, line1.Y1));
            }

            return IntersectPoints;
        }

        public override string ToString()
        {
            if (Backwards) return $"({X2}x{Y2})---({X1}x{Y1})";
            return $"({X1}x{Y1})---({X2}x{Y2})";
        }
    }
}
