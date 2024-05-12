namespace advent_of_code.Year2017.Day12
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            
            return GetGroupSize(Node.Parse(input), 0);
        }

        private static int GetGroupSize(Dictionary<int, Node> nodes, int groupItem)
        {
            var visited = new HashSet<int>();

            var queue = new Queue<int>();
            queue.Enqueue(groupItem);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Add(current))
                {
                    foreach (var subnode in nodes[current].ConnectsTo)
                    {
                        queue.Enqueue(subnode);
                    }
                }
            }

            return visited.Count;
        }
    }
}