namespace advent_of_code.Year2024.Day23
{
    internal class Node (string name)
    {
        public string Name { get; private set; } = name;
        public readonly List<string> ConnectedIDS = new List<string>();
    }
}
