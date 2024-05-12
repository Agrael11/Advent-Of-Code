namespace advent_of_code.Year2017.Day12
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            return CountGroups(Node.Parse(input));
        }

        private static int CountGroups(Dictionary<int, Node> nodes)
        {
            var groups = 0;

            var queue = new Queue<int>();
            while (nodes.Count > 0)
            {
                queue.Enqueue(nodes.Keys.First());
                groups++;

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (nodes.TryGetValue(current, out var node))
                    {
                        foreach (var subnode in node.ConnectsTo)
                        {
                            queue.Enqueue(subnode);
                        }
                        nodes.Remove(current);
                    }
                }
            }

            return groups;
        }
    }
}