namespace advent_of_code.Helpers
{
    public class Vector2d
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get => X; set => X = value; }
        public double Height { get => Y; set => Y = value; }

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2d(Vector2d vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector2d v) return false;

            return X == v.X && Y == v.Y;
        }

        public static double ManhattanDistance(Vector2d a, Vector2d b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static double Distance(Vector2d a, Vector2d b)
        {
            var s1 = a.X - b.X;
            var s2 = a.Y - b.Y;
            return Math.Sqrt(s1 * s1 +s2 * s2);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(X*X + Y*Y);
        }

        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0)
            {
                X /= magnitude;
                Y /= magnitude;
            }
        }

        public void SetMagnitude(double magnitude)
        {
            Normalize();
            X *= magnitude;
            Y *= magnitude;
        }

        public double GetHeading()
        {
            return Math.Atan2(Y, X);
        }

        public void SetHeading(double heading, double magnitude = 1)
        {
            X = Math.Cos(heading) * magnitude;
            Y = Math.Sin(heading) * magnitude;
        }

        public void RotateBy(double angle)
        {
            angle += GetHeading();
            var mag = GetMagnitude();
            SetHeading(angle, mag);
        }

        public static bool operator ==(Vector2d a, Vector2d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2d a, Vector2d b)
        {
            return !a.Equals(b);
        }

        public static Vector2d operator +(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2d operator -(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2d operator *(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2d operator /(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2d operator *(Vector2d v, double m)
        {
            return new Vector2d(v.X * m, v.Y * m);
        }
        public static Vector2d operator /(Vector2d v, double m)
        {
            return new Vector2d(v.X / m, v.Y / m);
        }

        public static implicit operator Vector2d(Vector2l v)
        {
            return new Vector2d(v.X, v.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X:0.##}-{Y:0.##})";
        }
    }
}
