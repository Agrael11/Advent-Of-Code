using System.Text;

namespace advent_of_code.Year2018.Day07
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            inputData = "Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var nodes = new Dictionary<char, Node>();

            foreach (var line in input)
            {
                var info = line.Split();
                var id1 = info[1][0];
                var id2 = info[7][0];
                if (!nodes.TryGetValue(id1, out var node1))
                {
                    node1 = new Node(id1);
                    nodes.Add(id1, node1);
                }
                if (!nodes.TryGetValue(id2, out var node2))
                {
                    node2 = new Node(id2);
                    nodes.Add(id2, node2);
                }
                node2.AddDependancy(id1);
                node1.AddDependant(id2);

            }

            nodes = nodes.OrderBy(n => n.Value.ID).ToDictionary();

            var result = new StringBuilder();
            while (nodes.Count > 0)
            {
                var firstEmptyNode = nodes.Where(n => n.Value.NoDependencies()).First().Value;
                var firstID = firstEmptyNode.ID;
                for (var depId = firstEmptyNode.Dependants.Count - 1; depId >= 0; depId--)
                {
                    var dependant = firstEmptyNode.Dependants[depId];
                    nodes[dependant].RemoveDependency(firstID);
                }
                result.Append(firstID);
                nodes.Remove(firstID);
            }

            return result.ToString();
        }
    }
}