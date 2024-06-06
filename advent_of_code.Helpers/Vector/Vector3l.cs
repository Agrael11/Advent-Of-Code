namespace advent_of_code.Helpers
{
    public class Vector3l
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public Vector3l(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3l(Vector3l vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector3l v) return false;

            return X == v.X && Y == v.Y && Z == v.Z;
        }

        public static double ManhattanDistance(Vector3l a, Vector3l b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        public static double Distance(Vector3l a, Vector3l b)
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
                X = (long)(X / magnitude);
                Y = (long)(Y / magnitude);
                Z = (long)(Z / magnitude);
            }
        }

        public void SetMagnitude(long magnitude)
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

        public void SetHeading(double azimuth, double elevation, long magnitude = 1)
        {
            X = (long)(Math.Cos(elevation) * Math.Cos(azimuth) * magnitude);
            Y = (long)(Math.Cos(elevation) * Math.Sin(azimuth) * magnitude);
            Z = (long)(Math.Sin(elevation) * magnitude);
        }

        public static bool operator ==(Vector3l a, Vector3l b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3l a, Vector3l b)
        {
            return !a.Equals(b);
        }

        public static Vector3l operator +(Vector3l a, Vector3l b)
        {
            return new Vector3l(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3l operator -(Vector3l a, Vector3l b)
        {
            return new Vector3l(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3l operator *(Vector3l a, Vector3l b)
        {
            return new Vector3l(a.X * b.X, a.Y * b.Y, a.Z  * b.Z);
        }
        public static Vector3l operator /(Vector3l a, Vector3l b)
        {
            return new Vector3l(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }
        public static Vector3l operator *(Vector3l v, long m)
        {
            return new Vector3l(v.X * m, v.Y * m, v.Z * m);
        }
        public static Vector3l operator /(Vector3l v, long m)
        {
            return new Vector3l(v.X / m, v.Y / m, v.Z / m);
        }

        public static explicit operator Vector3l(Vector3d v)
        {
            return new Vector3l((long)v.X, (long)v.Y, (long)v.Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X}-{Y}-{Z})";
        }
    }
}
