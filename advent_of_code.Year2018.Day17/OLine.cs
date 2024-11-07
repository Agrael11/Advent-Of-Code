using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace advent_of_code.Year2018.Day17
{
    internal class OLine
    {
        internal enum Orientations {Horizontal, Vertical};

        public int X1 { get; private set; }
        public int X2 { get; private set; }
        public int Y1 { get; private set; }
        public int Y2 { get; private set; }

        public bool LeftHit { get; set; } = false;
        public bool RightHit { get; set; } = false;

        public Orientations Orientation { get; private set; }

        public OLine(int x1, int x2, int y1, int y2)
        {
            if ((x1 != x2) && (y1 != y2)) throw new Exception("Not Orthogonal");

            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;

            if (X1 == X2) Orientation = Orientations.Vertical;
            else Orientation = Orientations.Horizontal;
        }

        public bool IsPointOnLine(int X, int Y)
        {
            if (Orientation == Orientations.Vertical)
            {
                return (X1 == X && Y >= Y1 && Y <= Y2);
            }

            return (X >= X1 && X <= X2 && Y == Y1);
        }

        public IEnumerable<(int, int)> GetAllPoints()
        {
            if (Orientation == Orientations.Vertical)
            {
                for (var i = Y1; i <= Y2; i++)
                    yield return (X1, i);
            }
            else
            {
                for (var i = X1; i <= X2; i++)
                    yield return (i, Y1);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not OLine otherLine) return false;

            return otherLine.X1 == X1 && otherLine.X2 == X2 && otherLine.Y1 == Y1 && otherLine.Y2 == Y2;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X1, X2, Y1, Y2);
        }

        public override string ToString()
        {
            return $"{((Orientation == Orientations.Horizontal)?"-":"|")} ({((Orientation == Orientations.Horizontal)?Y1:X1)});({((Orientation== Orientations.Horizontal)?X1:Y1)}-{((Orientation == Orientations.Horizontal) ? X2 : Y2)})";
        }

        public int GetLength()
        {
            if (Orientation == Orientations.Vertical) return (Y2 - Y1) + 1;
            return (X2 - X1) + 1;
        }

        public bool Intersects(OLine other)
        {
            return (other.X1 <= X2 && other.X2 >= X1 && other.Y1 <= Y2 && other.Y2 >= Y1);
        }
    }
}
