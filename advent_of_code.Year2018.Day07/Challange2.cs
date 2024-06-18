namespace advent_of_code.Year2018.Day07
{
    public static class Challange2
    {
        private static Dictionary<char, Node> nodes = new Dictionary<char, Node>();
        private static readonly List<char> active = new List<char>();

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            nodes.Clear();
            active.Clear();

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

            var workers = new List<Worker>();

            for (var i = 0; i < 5; i++)
            {
                workers.Add(new Worker(i));
            }

            var lastTime = 0L;

            while (nodes.Count > 0)
            {
                var readyForWork = workers.Where(w => w.WorkingAt == null).OrderBy(w => w.Time);
                var workersList = workers.Where(w => w.WorkingAt != null).OrderBy(w => w.Time);
                var available = nodes.Where(t => t.Value.NoDependencies() && !active.Contains(t.Key));
                if (readyForWork.Any() && available.Any())
                {
                    var worker = readyForWork.First();
                    worker.SetWork(available.First().Key, lastTime);
                    active.Add(available.First().Key);
                }
                else
                {
                    var worker = workersList.First();
                    lastTime = long.Max(worker.Time, lastTime);
                    if (worker.WorkingAt is not null)
                    {
                        SolveNode(worker.WorkingAt.Value);
                    }
                    worker.RemoveWork();
                }
            }

            return workers.OrderByDescending(w=>w.Time).First().Time;
        }

        private static void SolveNode(char nodeID)
        {
            var node = nodes[nodeID];
            for (var depId = node.Dependants.Count - 1; depId >= 0; depId--)
            {
                var dependant = node.Dependants[depId];
                nodes[dependant].RemoveDependency(nodeID);
            }
            nodes.Remove(nodeID);
            active.Remove(nodeID);
        }
    }
}