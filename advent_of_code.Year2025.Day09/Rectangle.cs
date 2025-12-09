namespace advent_of_code.Year2025.Day09
{

    /// <summary>
    /// Rectangle class - created of two opposite corners
    /// </summary>
    /// <param name="corner1"></param>
    /// <param name="corner2"></param>
    internal class Rectangle (Vertex corner1, Vertex corner2)
    {
        public Vertex Corner1 { get; set; } = corner1;
        public Vertex Corner2 { get; set; } = corner2;

        public long X1 => Math.Min(Corner1.X, Corner2.X);
        public long X2 => Math.Max(Corner1.X, Corner2.X);
        public long Y1 => Math.Min(Corner1.Y, Corner2.Y);
        public long Y2 => Math.Max(Corner1.Y, Corner2.Y);
        public long GetSize()
        {
            var xSide = X2 - X1 + 1;
            var ySide = Y2 - Y1 + 1;
            return xSide * ySide;
        }

        /// <summary>
        /// Checks if vertex is contained within the Rectangle
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Contains(Vertex vertex)
        {
            if (vertex == Corner1 || vertex == Corner2) return false;
            return (vertex.X >= X1 && vertex.X <= X2 && vertex.Y >= Y1 && vertex.Y <= Y2);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Corner1, Corner2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Rectangle other) return false;
            return (other.Corner1 == Corner1 && other.Corner2 == Corner2);
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"[{X1},{Y1}-{X2}-{Y2}]";
        }
    }
}
