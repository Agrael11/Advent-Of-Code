namespace advent_of_code.Helpers
{
    public class Trie<T> where T : notnull
    {
        private readonly Dictionary<T, Trie<T>> Branches = new Dictionary<T, Trie<T>>();


        public T Value { get; private set; }
        public Trie<T> Root { get; private set; }
        public Trie<T>? Parent { get; private set; }
        public bool IsEndOfTrie { get; set; }

        public Trie(T value, Trie<T> root, Trie<T>? parent = null, bool end = false)
        {
            Value = value;
            Root = root;
            Parent = parent;
            IsEndOfTrie = end;
        }

        public Trie(T value, Trie<T>? parent = null, bool end = false)
        {
            Value = value;
            Root = this;
            Parent = parent;
            IsEndOfTrie = end;
        }


        public bool TryGetBranch(T value, out Trie<T>? branch)
        {
            return Branches.TryGetValue(value, out branch);
        }

        public bool ContainsBranch(T value)
        {
            return Branches.ContainsKey(value);
        }

        public List<T> GetBranchKeys()
        {
            return Branches.Keys.ToList();
        }

        public Trie<T> GetOrAddBranch(T value, bool end = false)
        {
            if (Branches.TryGetValue(value, out var branch)) return branch;

            branch = new Trie<T>(value, Root, this, end);
            Branches.Add(value, branch);
            return branch;
        }

        public override string ToString()
        {
            return $"[{Value}] -> {{{string.Join(';', Branches.Keys)}}}";
        }
    }
}
