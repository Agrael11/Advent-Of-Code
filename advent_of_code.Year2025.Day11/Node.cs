namespace advent_of_code.Year2025.Day11
{
    /// <summary>
    /// Simple Node - containsn name and it's connected nodes
    /// </summary>
    /// <param name="name"></param>
    internal class Node (string name)
    {
        public string Name { get; } = name;
        public List<Node> ConnectedNodes { get; } = new List<Node>();

        /// <summary>
        /// Just to make debugging easier
        /// </summary>
        /// <returns>Stringified version of node</returns>
        public override string ToString()
        {
            return $"{Name}{((ConnectedNodes.Count > 0)?" => ":"")}{string.Join(", ", ConnectedNodes.Select(t=>t.Name))}";
        }
    }
}
