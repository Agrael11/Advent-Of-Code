namespace advent_of_code.Helpers
{
    public class Vector3d
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d(Vector3d vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector3d v) return false;

            return X == v.X && Y == v.Y && Z == v.Z;
        }

        public static double ManhattanDistance(Vector3d a, Vector3d b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        public static double Distance(Vector3d a, Vector3d b)
        {
            var s1 = a.X - b.X;
            var s2 = a.Y - b.Y;
            var s3 = a.Z - b.Z;
            return Math.Sqrt(s1 * s1 + s2 * s2 + s3*s3);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(X*X + Y*Y + Z*Z);
        }

        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0)
            {
                X /= magnitude;
                Y /= magnitude;
                Z /= magnitude;
            }
        }

        public void SetMagnitude(double magnitude)
        {
            Normalize();
            X *= magnitude;
            Y *= magnitude;
            Z *= magnitude;
        }

        public (double azimuth, double elevation) GetHeading()
        {
            var azimuth = Math.Atan2(Y, X);
            var magnitude = GetMagnitude();
            var elevation = Math.Asin(Z / magnitude);
            return (azimuth, elevation);
        }

        public void SetHeading(double azimuth, double elevation, double magnitude = 1)
        {
            X = Math.Cos(elevation) * Math.Cos(azimuth) * magnitude;
            Y = Math.Cos(elevation) * Math.Sin(azimuth) * magnitude;
            Z = Math.Sin(elevation) * magnitude;
        }

        public static bool operator ==(Vector3d a, Vector3d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3d a, Vector3d b)
        {
            return !a.Equals(b);
        }

        public static Vector3d operator +(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3d operator -(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3d operator *(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X * b.X, a.Y * b.Y, a.Z  * b.Z);
        }
        public static Vector3d operator /(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }
        public static Vector3d operator *(Vector3d v, double m)
        {
            return new Vector3d(v.X * m, v.Y * m, v.Z * m);
        }
        public static Vector3d operator /(Vector3d v, double m)
        {
            return new Vector3d(v.X / m, v.Y / m, v.Z / m);
        }

        public static implicit operator Vector3d(Vector3l v)
        {
            return new Vector3d(v.X, v.Y, v.Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X:0.##}-{Y:0.##}-{Z:0.##})";
        }
    }
}
