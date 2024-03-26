namespace advent_of_code.Year2016.Day17
{
    public class Position(int x, int y, string path)
    {
        public readonly int X = x;
        public readonly int Y = y;
        public readonly string Path = path;

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Path);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Position position) return false;
            return X == position.X && Y == position.Y && Path == position.Path;
        }
    }
}
