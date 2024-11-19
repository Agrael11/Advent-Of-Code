using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day23
{
    internal class Box (long x, long y, long z, long x2, long y2, long z2)
    {
        public long X { get; set; } = x;
        public long Y { get; set; } = y;
        public long Z { get; set; } = z;
        public long X2 = x2;
        public long Y2 = y2;
        public long Z2 = z2;
        public long Width => X2 - X;
        public long Height => Y2 - Y;
        public long Depth => Z2 - Z;
        public long Size => Width * Height * Depth;

        public bool Contains(Vector3l point)
        {
            return (X >= point.X && Y >= point.Y && Z >= point.Z && X2 <= point.X && Y2 <= point.Y && Z2 <= point.Z);
        }

        public bool Intersects(Vector3l point, long radius)
        {
            if (Contains(point)) return true;
            var closeX = Math.Clamp(point.X, X, X2);
            var closeY = Math.Clamp(point.Y, Y, Y2);
            var closeZ = Math.Clamp(point.Z, Z, Z2);
            return (Vector3l.ManhattanDistance(point, closeX, closeY, closeZ) <= radius);
        }

        public List<Box> Divide()
        {
            var result = new List<Box>();

            var xSplit = X + Width / 2;
            var ySplit = Y + Height / 2;
            var zSplit = Z + Depth / 2;

            result.Add(new Box(X, Y, Z, xSplit, ySplit, zSplit));
            if (xSplit + 1 <= X2) result.Add(new Box(xSplit + 1, Y, Z, X2, ySplit, zSplit));
            if (ySplit + 1 <= Y2) result.Add(new Box(X, ySplit + 1, Z, xSplit, Y2, zSplit));
            if (xSplit + 1 <= X2 && ySplit + 1 <= Y2) result.Add(new Box(xSplit + 1, ySplit + 1, Z, X2, Y2, zSplit));
            if (zSplit + 1 <= Z2) result.Add(new Box(X, Y, zSplit + 1, xSplit, ySplit, Z2));
            if (xSplit + 1 <= X2 && zSplit + 1 <= Z2) result.Add(new Box(xSplit + 1, Y, zSplit + 1, X2, ySplit, Z2));
            if (ySplit + 1 <= Y2 && zSplit + 1 <= Z2) result.Add(new Box(X, ySplit + 1, zSplit + 1, xSplit, Y2, Z2));
            if (xSplit + 1 <= X2 && ySplit + 1 <= Y2 && zSplit + 1 <= Z2) result.Add(new Box(xSplit + 1, ySplit + 1, zSplit + 1, X2, Y2, Z2));

            return result;
        }

        public override string ToString()
        {
            return $"{X}-{X2}; {Y}-{Y2}; {Z}-{Z2}";
        }
    }
}
