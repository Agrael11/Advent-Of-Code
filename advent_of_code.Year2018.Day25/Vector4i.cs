using System.Runtime.Serialization;

namespace advent_of_code.Year2018.Day25
{
    internal class Vector4i (int x, int y, int z, int t)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Z { get; set; } = z;
        public int T { get; set; } = t;

        public static int Manhattan(Vector4i one, Vector4i two)
        {
            return Math.Abs(one.X - two.X) + Math.Abs(one.Y - two.Y) + Math.Abs(one.Z - two.Z) + Math.Abs(one.T - two.T);
        }

        public override string ToString()
        {
            return $"{X}:{Y}:{Z}:{T}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X,Y,Z,T);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector4i other) return false;
            return (other.X == X) && (other.Y == Y) && (other.Z == Z) && (other.T == T);
        }

        public static bool operator ==(Vector4i left, Vector4i right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4i left, Vector4i right)
        {
            return !left.Equals(right);
        }
    }
}
