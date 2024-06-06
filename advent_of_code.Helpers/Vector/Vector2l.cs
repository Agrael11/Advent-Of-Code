namespace advent_of_code.Helpers
{
    public class Vector2l
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Width { get => X; set => X = value; }
        public long Height { get => Y; set => Y = value; }

        public Vector2l(long x, long y)
        {
            X = x;
            Y = y;
        }


        public Vector2l(Vector2l vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector2l v) return false;

            return X == v.X && Y == v.Y;
        }

        public static long ManhattanDistance(Vector2l a, Vector2l b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static double Distance(Vector2l a, Vector2l b)
        {
            var s1 = a.X - b.X;
            var s2 = a.Y - b.Y;
            return Math.Sqrt(s1 * s1 + s2 * s2);
        }

        public static long ManhattanDistance(Vector2l a, long x, long y)
        {
            return Math.Abs(a.X - x) + Math.Abs(a.Y - y);
        }

        public static double Distance(Vector2l a, long x, long y)
        {
            var s1 = a.X - x;
            var s2 = a.Y - y;
            return Math.Sqrt(s1 * s1 + s2 * s2);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0)
            {
                X = (long)(X/magnitude);
                Y = (long)(Y/magnitude);
            }
        }

        public void SetMagnitude(long magnitude)
        {
            var oldMagnitude = GetMagnitude();
            if (oldMagnitude != 0)
            {
                X = (long)((magnitude*X) / oldMagnitude);
                Y = (long)((magnitude*Y) / oldMagnitude);
            }
        }

        public double GetHeading()
        {
            return Math.Atan2(Y, X);
        }

        public void SetHeading(double heading, long magnitude = 1)
        {
            X = (long)(Math.Cos(heading) * magnitude);
            Y = (long)(Math.Sin(heading) * magnitude);
        }

        public void RotateBy(double angle)
        {
            angle += GetHeading();
            var mag = GetMagnitude();
            X = (long)(Math.Cos(angle) * mag);
            Y = (long)(Math.Sin(angle) * mag);
        }

        public static bool operator ==(Vector2l a, Vector2l b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2l a, Vector2l b)
        {
            return !a.Equals(b);
        }

        public static Vector2l operator +(Vector2l a, Vector2l b)
        {
            return new Vector2l(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2l operator -(Vector2l a, Vector2l b)
        {
            return new Vector2l(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2l operator *(Vector2l a, Vector2l b)
        {
            return new Vector2l(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2l operator /(Vector2l a, Vector2l b)
        {
            return new Vector2l(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2l operator *(Vector2l v, long m)
        {
            return new Vector2l(v.X * m, v.Y * m);
        }
        public static Vector2l operator /(Vector2l v, long m)
        {
            return new Vector2l(v.X / m, v.Y / m);
        }

        public static explicit operator Vector2l(Vector2d v)
        {
            return new Vector2l((long)v.X, (long)v.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X}-{Y})";
        }
    }
}
