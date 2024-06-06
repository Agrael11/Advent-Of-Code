namespace advent_of_code.Helpers.Rectangle
{
    public class RectangleL
    {
        public Vector2l Position { get; set; }
        public Vector2l Size { get; set; }
        public long X { get => Position.X; set => Position.X = value; }
        public long Y { get => Position.Y; set => Position.Y = value; }
        public long Width { get => Size.Width; set => Size.Width = value; }
        public long Height { get => Size.Height; set => Size.Height = value; }
        public long X1 { get => Position.X; set => Position.X = value; }
        public long Y1 { get => Position.Y; set => Position.Y = value; }
        public long X2 => Position.X + Size.Width;
        public long Y2 => Position.Y + Size.Height;

        public RectangleL(long x, long y, long width, long height)
        {
            Position =new Vector2l(x, y);
            Size = new Vector2l(width, height);
        }
        public RectangleL(Vector2l position, long width, long height)
        {
            Position = position;
            Size = new Vector2l(width, height);
        }
        public RectangleL(long x, long y, Vector2l size)
        {
            Position = new Vector2l(x, y);
            Size = size;
        }
        public RectangleL(Vector2l position, Vector2l size)
        {
            Position = position;
            Size = size;
        }

        public static bool Intersects(RectangleL rectangle1, RectangleL rectangle2)
        {
            return (rectangle1.X <= rectangle2.X2 && rectangle1.X2 >= rectangle2.X &&
                rectangle1.Y <= rectangle2.Y2 && rectangle1.Y2 >= rectangle2.Y);
        }

        public static bool Contains(RectangleL rectangle, Vector2l point)
        {
            return (point.X >= rectangle.X && point.X <= rectangle.X2 &&
                point.Y >= rectangle.Y && point.Y <= rectangle.Y2);
        }
        public static bool Contains(RectangleL rectangle1, RectangleL rectangle2)
        {
            return (rectangle1.X >= rectangle2.X && rectangle1.X2 <= rectangle2.X2 &&
                rectangle1.Y >= rectangle2.Y && rectangle1.Y2 <= rectangle2.Y2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not RectangleL r) return false;

            return Position == r.Position && Size == r.Size;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Size);
        }

        public static bool operator ==(RectangleL r1, RectangleL r2)
        {
            return r1.Equals(r2);
        }
        public static bool operator !=(RectangleL r1, RectangleL r2)
        {
            return !r1.Equals(r2);
        }

        public static explicit operator RectangleL(RectangleD d)
        {
            return new RectangleL((Vector2l)d.Position, (Vector2l)d.Size);
        }
    }
}
