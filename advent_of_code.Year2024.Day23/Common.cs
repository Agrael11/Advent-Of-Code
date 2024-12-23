namespace advent_of_code.Year2024.Day23
{
    internal static class Common
    {
        public static readonly Dictionary<string ,Node> Nodes = new Dictionary<string, Node>();

        public static void Parse(string[] input)
        {
            Nodes.Clear();

            foreach (var splitLine in input.Select(line => line.Split('-')))
            {
                var first = splitLine[0];
                var second = splitLine[1];
                if (!Nodes.TryGetValue(first, out var node))
                {
                    node = new Node(first);
                    Nodes.Add(first, node);
                }
                node.ConnectedIDS.Add(second);
                if (!Nodes.TryGetValue(second, out node))
                {
                    node = new Node(second);
                    Nodes.Add(second, node);
                }
                node.ConnectedIDS.Add(first);
            }
        }
    }
}
