namespace advent_of_code.Year2025.Day09
{
    /// <summary>
    /// Edge - technnically similar to Rectangle, but should only be 1 wide/tall, and always orthogonal
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    internal class Edge(Vertex first, Vertex second)
    {
        public long X1 { get; } = long.Min(first.X, second.X);
        public long X2 { get; } = long.Max(first.X, second.X);
        public long Y1 { get; } = long.Min(first.Y, second.Y);
        public long Y2 { get; } = long.Max(first.Y, second.Y);
        public bool Horizontal => Y1 == Y2;
        public bool Vertical => X1 == X2;


        /// <summary>
        /// Checks if Edge is inside of Rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsInsideRectangle(Rectangle rectangle)
        {
            if (Vertical)
            {
                return ((X1 < rectangle.X2 && X2 > rectangle.X1) &&
                    (Y1 < rectangle.Y2 && Y2 > rectangle.Y1));
            }
            if (Horizontal)
            {
                return ((Y2 > rectangle.Y1 && Y1 < rectangle.Y2) &&
                    (X1 < rectangle.X2 && X2 > rectangle.X1));
            }
            throw new Exception("This is not Vertical nor Horizontal edge. In other words. You messed up parsing.");
        }



        public override int GetHashCode()
        {
            return HashCode.Combine(X1, Y1, X2, Y2);
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Edge other) return false;
            return X1 == other.X1 && X2 == other.X2 && Y1 == other.Y1 && Y2 == other.Y2;
        }
        public static bool operator ==(Edge left, Edge right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Edge left, Edge right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"[{X1},{Y1}<->{X2},{Y2}]";
        }
    }
}
