namespace advent_of_code.Year2018.Day03
{
    internal class Rectangle
    {
        public bool Collided { get; set; } = false;
        public int ID { get; }
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }
        public int Width { get; }
        public int Height { get; }
        public Rectangle (int id, int x, int y, int width, int height)
        {
            ID = id;
            X1 = x;
            Y1 = y;
            Width = width;
            Height = height;
            X2 = x + width - 1;
            Y2 = y + height - 1;
        }
        public Rectangle (string definition)
        {
            var rectangle = definition.Split(' ');
            ID = int.Parse(rectangle[0].TrimStart('#'));
            var position = rectangle[2].TrimEnd(':').Split(',');
            X1 = int.Parse(position[0]);
            Y1 = int.Parse(position[1]);
            var size = rectangle[3].Split('x');
            Width = int.Parse(size[0]);
            Height = int.Parse(size[1]);
            X2 = X1 + Width - 1;
            Y2 = Y1 + Height - 1;
        }

        public bool Intersects(Rectangle other)
        {
            return (X1 <= other.X2 && X2 >= other.X1 && Y1 <= other.Y2 && Y2 >= other.Y1);
        }

        public Rectangle? Overlap(Rectangle other)
        {
            if (!Intersects(other)) return null;

            var x1 = int.Max(X1, other.X1);
            var x2 = int.Min(X2, other.X2);
            var y1 = int.Max(Y1, other.Y1);
            var y2 = int.Min(Y2, other.Y2);

            return new Rectangle(-1, x1, y1, x2-x1+1, y2-y1+1);
        }
    }
}
