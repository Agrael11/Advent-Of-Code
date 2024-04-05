namespace advent_of_code.Year2016.Day22
{
    public class DataNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Available => Size - Used;
        public int UsagePercent => (Used * 100 / Size);

        public DataNode(int x, int y, int size, int used)
        {
            X = x;
            Y = y;
            Size = size;
            Used = used;
        }

        public DataNode(DataNode node)
        {
            X = node.X;
            Y = node.Y;
            Size = node.Size;
            Used = node.Used;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not DataNode dataNode) return false;
            return dataNode.X == X && dataNode.Y == Y && dataNode.Used == Used && dataNode.Size == Size;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Used, Size);
        }
    }
}
