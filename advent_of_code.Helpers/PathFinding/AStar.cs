namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        public static (T? type, int cost) DoAStar<T>(T startPoint, Func<T, bool> isEnd,
            Func<T, IEnumerable<(T nextState, int cost)>> getNext, Func<T, int> heuristic, int costWeight) where T : notnull
        {
            var queue = new PriorityQueue<T, int>();
            var visited = new HashSet<T>();
            var gScore = new Dictionary<T, int>();
            var fScore = new Dictionary<T, int>();

            gScore.Add(startPoint, 0);
            fScore.Add(startPoint, heuristic(startPoint));
            queue.Enqueue(startPoint, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (isEnd(current))
                {
                    return (current, gScore[current]);
                }

                foreach (var nextState in getNext(current))
                {
                    if (visited.Contains(nextState.nextState))
                    {
                        continue;
                    }

                    var tentativeScore = gScore[current] + nextState.cost;

                    if (!gScore.TryGetValue(nextState.nextState, out var nextStateCost) || tentativeScore < nextStateCost)
                    {
                        var fullCost = tentativeScore + costWeight * heuristic(nextState.nextState);

                        gScore[nextState.nextState] = tentativeScore;
                        fScore[nextState.nextState] = fullCost;

                        queue.Enqueue(nextState.nextState, fullCost);
                    }
                }

                visited.Add(current);
            }

            return (default, -1);
        }

        public static (List<T>? type, int cost) DoAStarWithPath<T>(T startPoint, Func<T, bool> isEnd,
            Func<T, IEnumerable<(T nextState, int cost)>> getNext, Func<T, int> heuristic, int costWeight) where T : notnull
        {
            var queue = new PriorityQueue<T, int>();
            var visited = new HashSet<T>();
            var gScore = new Dictionary<T, int>();
            var fScore = new Dictionary<T, int>();
            var parents = new Dictionary<T, T>();

            gScore.Add(startPoint, 0);
            fScore.Add(startPoint, heuristic(startPoint));
            queue.Enqueue(startPoint, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (isEnd(current))
                {
                    return (ReconstructPath(parents, current), gScore[current]);
                }

                foreach (var nextState in getNext(current))
                {
                    if (visited.Contains(nextState.nextState))
                    {
                        continue;
                    }

                    var tentativeScore = gScore[current] + nextState.cost;

                    if (!gScore.TryGetValue(nextState.nextState, out var nextStateCost) || tentativeScore < nextStateCost)
                    {
                        var fullCost = tentativeScore + costWeight * heuristic(nextState.nextState);

                        gScore[nextState.nextState] = tentativeScore;
                        fScore[nextState.nextState] = fullCost;
                        parents[nextState.nextState] = current;

                        queue.Enqueue(nextState.nextState, fullCost);
                    }
                }

                visited.Add(current);
            }

            return (null, -1);
        }
    }
}