namespace advent_of_code.Helpers.Rectangle
{
    public class RectangleD
    {
        public Vector2d Position { get; set; }
        public Vector2d Size { get; set; }
        public double X { get => Position.X; set => Position.X = value; }
        public double Y { get => Position.Y; set => Position.Y = value; }
        public double Width { get => Size.Width; set => Size.Width = value; }
        public double Height { get => Size.Height; set => Size.Height = value; }
        public double X1 { get => Position.X; set => Position.X = value; }
        public double Y1 { get => Position.Y; set => Position.Y = value; }
        public double X2 => Position.X + Size.Width;
        public double Y2 => Position.Y + Size.Height;

        public RectangleD(double x, double y, double width, double height)
        {
            Position =new Vector2d(x, y);
            Size = new Vector2d(width, height);
        }
        public RectangleD(Vector2d position, double width, double height)
        {
            Position = position;
            Size = new Vector2d(width, height);
        }
        public RectangleD(double x, double y, Vector2d size)
        {
            Position = new Vector2d(x, y);
            Size = size;
        }
        public RectangleD(Vector2d position, Vector2d size)
        {
            Position = position;
            Size = size;
        }

        public static bool Intersects(RectangleD rectangle1, RectangleD rectangle2)
        {
            return (rectangle1.X <= rectangle2.X2 && rectangle1.X2 >= rectangle2.X &&
                rectangle1.Y <= rectangle2.Y2 && rectangle1.Y2 >= rectangle2.Y);
        }

        public static bool Contains(RectangleD rectangle, Vector2d point)
        {
            return (point.X >= rectangle.X && point.X <= rectangle.X2 &&
                point.Y >= rectangle.Y && point.Y <= rectangle.Y2);
        }
        public static bool Contains(RectangleD rectangle1, RectangleD rectangle2)
        {
            return (rectangle1.X >= rectangle2.X && rectangle1.X2 <= rectangle2.X2 &&
                rectangle1.Y >= rectangle2.Y && rectangle1.Y2 <= rectangle2.Y2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not RectangleD r) return false;

            return Position == r.Position && Size == r.Size;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Size);
        }

        public static bool operator ==(RectangleD r1, RectangleD r2)
        {
            return r1.Equals(r2);
        }
        public static bool operator !=(RectangleD r1, RectangleD r2)
        {
            return !r1.Equals(r2);
        }

        public static implicit operator RectangleD(RectangleL d)
        {
            return new RectangleD(d.Position, d.Size);
        }
    }
}
