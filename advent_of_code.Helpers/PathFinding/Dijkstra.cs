namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        public static (T? type, int cost) DoDijkstra<T>(T startPoint, Func<T, bool> isEnd,
            Func<T, IEnumerable<(T nextState, int cost)>> getNext) where T : notnull
        {
            var queue = new PriorityQueue<T, int>();
            var costs = new Dictionary<T, int>();

            queue.Enqueue(startPoint, 0);
            costs.Add(startPoint, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (isEnd(current))
                {
                    return (current, costs[current]);
                }

                foreach (var nextState in getNext(current))
                {
                    var tentativeCost = costs[current] + nextState.cost;

                    if (!costs.TryGetValue(nextState.nextState, out var oldCost) || tentativeCost < oldCost)
                    {
                        queue.Enqueue(nextState.nextState, nextState.cost);
                        costs[nextState.nextState] = tentativeCost;
                    }
                }
            }

            return (default, -1);
        }

        public static (List<T>? type, int cost) DoDijkstraWithPath<T>(T startPoint,
            Func<T, bool> isEnd, Func<T, IEnumerable<(T nextState, int cost)>> getNext) where T : notnull
        {
            var queue = new PriorityQueue<T, int>();
            var costs = new Dictionary<T, int>();
            var parents = new Dictionary<T, T>();

            queue.Enqueue(startPoint, 0);
            costs.Add(startPoint, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (isEnd(current))
                {
                    return (ReconstructPath(parents, current), costs[current]);
                }

                foreach (var nextState in getNext(current))
                {
                    var tentativeCost = costs[current] + nextState.cost;

                    if (!costs.TryGetValue(nextState.nextState, out var oldCost) || tentativeCost < oldCost)
                    {
                        queue.Enqueue(nextState.nextState, nextState.cost);
                        parents[nextState.nextState] = current;
                        costs[nextState.nextState] = tentativeCost;
                    }
                }
            }

            return (null, -1);
        }
    }
}