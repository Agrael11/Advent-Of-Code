namespace advent_of_code.Year2019.Day06
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L\r\nK)YOU\r\nI)SAN";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t => t.Split(')'));

            var nodes = new Dictionary<string, Node>();
            foreach (var connection in input)
            {
                if (!nodes.TryGetValue(connection[0], out var source))
                {
                    source = new Node(connection[0]);
                    nodes.Add(connection[0], source);
                }
                if (!nodes.TryGetValue(connection[1], out var target))
                {
                    target = new Node(connection[1]);
                    nodes.Add(connection[1], target);
                }
                source.AddChild(target);
            }

            var you = nodes["YOU"];
            var san = nodes["SAN"];
            if (you.Parent is null || san.Parent is null) throw new Exception("Why are we floating in space?");
            return BFS(you.Parent, san.Parent);
        }

        private static int BFS(Node startNode, Node endNode)
        {
            var queue = new Queue<(Node node, int jumps)>();
            var visited = new HashSet<Node>();
            queue.Enqueue((startNode, 0));
            while (queue.Count > 0)
            {
                var (node, jumps) = queue.Dequeue();
                if (node== endNode)
                {
                    return jumps;
                }
                if (node.Parent != null && visited.Add(node.Parent))
                {
                    queue.Enqueue((node.Parent, jumps + 1));
                }
                foreach (var child in node.Children)
                {
                    if (visited.Add(child))
                    {
                        queue.Enqueue((child, jumps + 1));
                    }
                }
            }

            return -1;
        }
    }
}