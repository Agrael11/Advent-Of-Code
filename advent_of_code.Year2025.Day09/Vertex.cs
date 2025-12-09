namespace advent_of_code.Year2025.Day09
{
    /// <summary>
    /// Single Vertex in Polygon/Rectangle
    /// </summary>
    internal class Vertex
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Vertex(long x, long y)
        {
            X = x;
            Y = y;
        }

        public Vertex(string input)
        {
            var split = input.Split(',');
            X = long.Parse(split[0]);
            Y = long.Parse(split[1]);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vertex other) return false;
            return (other.X == X && other.Y == Y);
        }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}
