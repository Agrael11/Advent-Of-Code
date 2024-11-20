namespace advent_of_code.Year2018.Day25
{
    internal class Node (Vector4i position)
    {
        public Vector4i Position { get; set; } = position;
        public readonly List<Node> Connected = new List<Node>();

        public override string ToString()
        {
            return $"{Position}";
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Node node) return false;
            return node.Position.Equals(Position);
        }

        public static bool operator ==(Node left, Node right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !left.Equals(right);
        }
    }
}
