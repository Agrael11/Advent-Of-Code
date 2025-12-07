namespace advent_of_code.Year2025.Day07
{
    internal class Manifold (int x, int y)
    {
        public (int X, int Y) Position { get; } = (x, y);
        public bool Visited { get; private set; } = false;
        
        public long Value => GetValue();


        private long? _value = null;
        private readonly List<Manifold> ConnectedManifolds = new List<Manifold>();

        public void Visit()
        {
            Visited = true;
        }

        /// <summary>
        /// Adds connection and resets memoization
        /// </summary>
        /// <param name="manifold"></param>
        public void AddConnection(Manifold manifold)
        {
            ConnectedManifolds.Add(manifold);
            manifold.Visit();
            _value = null;
        }

        /// <summary>
        /// Gets value of manifold = sum of values of all manifolds it connects to + 1 for itself.
        /// It memoizes this value so it's much faster
        /// </summary>
        /// <returns></returns>
        public long GetValue()
        {
            _value ??= (ConnectedManifolds.Sum(t => t.Value) + 1);
            return _value.Value;
        }
    }
}
