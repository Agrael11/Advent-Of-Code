namespace advent_of_code.Helpers
{
    public class Trie<T>(T value) where T : notnull
    {
        public T Value { get; private set; } = value;
        private readonly Dictionary<T, Trie<T>> Branches = new Dictionary<T, Trie<T>>();

        public bool TryGetBranch(T value, out Trie<T>? branch)
        {
            return Branches.TryGetValue(value, out branch);
        }

        public bool ContainsBranch(T value)
        {
            return Branches.ContainsKey(value);
        }

        public Trie<T> GetOrAddBranch(T value)
        {
            if (Branches.TryGetValue(value, out var branch)) return branch;

            branch = new Trie<T>(value);
            Branches.Add(value, branch);
            return branch;
        }

        public override string ToString()
        {
            return $"[{Value}] -> {{{string.Join(';', Branches.Keys)}}}";
        }
    }
}
