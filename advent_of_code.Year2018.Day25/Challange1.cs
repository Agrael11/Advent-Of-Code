namespace advent_of_code.Year2018.Day25
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var nodes = new List<Node>();
            foreach (var line in input)
            {
                var nodeData = line.Split(',').Select(int.Parse).ToArray();
                nodes.Add(new Node(new Vector4i(nodeData[0], nodeData[1], nodeData[2], nodeData[3])));
            }

            for (var nodeIndex1 = 0; nodeIndex1 < nodes.Count - 1; nodeIndex1++)
            {
                var node1 = nodes[nodeIndex1];
                for (var nodeIndex2 = nodeIndex1 + 1; nodeIndex2 < nodes.Count; nodeIndex2++)
                {
                    var node2 = nodes[nodeIndex2];
                    if (Vector4i.Manhattan(node1.Position, node2.Position) <= 3)
                    {
                        node1.Connected.Add(node2);
                        node2.Connected.Add(node1);
                    }
                }
            }

            var groups = 0;
            while (nodes.Count > 0)
            {
                groups++;
                RemoveGroup(nodes);
            }

            return groups;
        }

        private static void RemoveGroup(List<Node> nodes)
        {
            var stack = new Stack<Node>();
            stack.Push(nodes[0]);
            nodes.RemoveAt(0);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                foreach (var connection in current.Connected)
                {
                    if (nodes.Remove(connection))
                    {
                        stack.Push(connection);
                    }
                }
            }
        }
    }
}