using System.Threading.Tasks.Dataflow;

namespace advent_of_code.Year2019.Day06
{
    internal class Node (string name, Node? parent = null)
    {
        public string Name { get; set; } = name;
        public Node? Parent { get; set; } = parent;
        public readonly List<Node> Children = new List<Node>();

        private int? _distance = null;
        public int Distance {
            get 
            {
                _distance ??= ((Parent is null) ? 0 : (Parent.Distance + 1));
                return _distance.Value;
            } 
        }

        public void SetParent(Node node, bool recursive = true)
        {
            if (recursive)
            {
                node.AddChild(this, false);
            }
            Parent = node;
        }
        public void AddChild(Node node, bool recursive = true)
        {
            if (recursive)
            {
                node.SetParent(this, false);
            }
            Children.Add(node);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Node other) return false;
            return other.Name == Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Distance);
        }

        public override string ToString()
        {
            if (Parent is null) return Name;
            return $"{Parent.Name}){Name}";
        }
    }
}
