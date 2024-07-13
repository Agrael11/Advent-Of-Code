using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day10
{
    internal class Point
    {
        public (long X, long Y) Position { get; set; }
        public (long X, long Y) Velocity { get; private set; }

        public Point(string definition)
        {
            var data = definition.Split('<');
            var posInfo = data[1][.. data[1].IndexOf('>')].Split(',');
            var velInfo = data[2][..^1].Split(',');
            Position = (long.Parse(posInfo[0]), long.Parse(posInfo[1]));
            Velocity= (long.Parse(velInfo[0]), long.Parse(velInfo[1]));
        }

        public Point((long X, long Y) position, (long X, long Y) velocity) 
        {
            Position = (position);
            Velocity = (velocity);
        }

        public Point GetAfter(long time)
        {
            var pX = Position.X + Velocity.X * time;
            var pY = Position.Y + Velocity.Y * time;
            return new Point((pX, pY), Velocity);
        }

        public static List<Point> GetAfter(List<Point> points, long time)
        {
            var newPoints = new List<Point>();
            foreach (var point in points)
            {
                newPoints.Add(point.GetAfter(time));
            }
            return newPoints;
        }

        public static long GetHeight(List<Point> points)
        {
            var height = points.Max(p => p.Position.Y) - points.Min(p => p.Position.Y) + 1;
            return height;
        }

        public static long GetHeightAfter(List<Point> points, long time)
        {
            var newPoints = GetAfter(points, time);
            return GetHeight(newPoints);
        }
    }
}
