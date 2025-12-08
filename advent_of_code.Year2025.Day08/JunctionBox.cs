namespace advent_of_code.Year2025.Day08
{
    internal class JunctionBox
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public JunctionBox(long x, long y, long z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Parses Junctionbox from string
        /// </summary>
        /// <param name="definition">String Defintion of Junction (3 numbers split by comma)</param>
        public JunctionBox(string definition)
        {
            var split = definition.Split(',');
            X = long.Parse(split[0]);
            Y = long.Parse(split[1]);
            Z = long.Parse(split[2]);
        }

        /// <summary>
        /// Calculates distance to other JunctionBox - pythagoras 3D
        /// </summary>
        /// <param name="other">Second JunctionBox</param>
        /// <returns>Distance</returns>
        public double GetDistanceTo(JunctionBox other)
        {
            var xDist = X - other.X;
            var yDist = Y - other.Y;
            var zDist = Z - other.Z;
            return Math.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X,Y,Z);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not JunctionBox other) return false;
            return (other.X == X && other.Y == Y && other.Z == Z);
        }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }

        public static bool operator ==(JunctionBox left, JunctionBox right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(JunctionBox left, JunctionBox right)
        {
            return !left.Equals(right);
        }
    }
}