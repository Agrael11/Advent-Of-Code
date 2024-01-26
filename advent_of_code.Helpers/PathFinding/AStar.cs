namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        public static (T? type, int cost) DoAStar<T>(T startPoint, Func<T, bool> isEnd,
            Func<T, IEnumerable<(T nextState, int cost)>> getNext, Func<T, int> heuristic, int costWeight) where T : notnull
        {
            PriorityQueue<T, int> queue = new();
            HashSet<T> visited = [];
            Dictionary<T, int> gScore = [];
            Dictionary<T, int> fScore = [];

            gScore.Add(startPoint, 0);
            fScore.Add(startPoint, heuristic(startPoint));
            queue.Enqueue(startPoint, 0);

            while (queue.Count > 0)
            {
                T current = queue.Dequeue();

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

                    if (!gScore.TryGetValue(nextState.nextState, out int nextStateCost) || tentativeScore < nextStateCost)
                    {
                        int fullCost = tentativeScore + costWeight * heuristic(nextState.nextState);

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
            T? current = startPoint;
            PriorityQueue<T, int> queue = new();
            HashSet<T> visited = [];
            Dictionary<T, int> gScore = [];
            Dictionary<T, int> fScore = [];
            Dictionary<T, T> parents = [];

            gScore.Add(startPoint, 0);
            fScore.Add(startPoint, heuristic(startPoint));
            queue.Enqueue(startPoint, 0);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();

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

                    if (!gScore.TryGetValue(nextState.nextState, out int nextStateCost) || tentativeScore < nextStateCost)
                    {
                        int fullCost = tentativeScore + costWeight * heuristic(nextState.nextState);

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