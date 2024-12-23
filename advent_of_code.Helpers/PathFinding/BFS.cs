﻿namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        public static (T? type, int length) DoBFS<T>(T startPoint, Func<T, bool> isEnd, Func<T, IEnumerable<T>> getNext) where T : notnull
        {
            var queue = new Queue<T>();
            var visited = new Dictionary<T,int>();

            queue.Enqueue(startPoint);
            visited.Add(startPoint, 0);
            while (queue.Count > 0) 
            {
                var current = queue.Dequeue();
                
                if (isEnd(current))
                {
                    return (current, visited[current]);
                }

                foreach (var next in getNext(current)) 
                {
                    if (visited.ContainsKey(next)) continue;

                    queue.Enqueue(next);
                    visited.Add(next, visited[current] + 1);
                }
            }

            return (default, -1);
        }

        public static (List<T>? path, int length) DoBFSWithPath<T>(T startPoint, Func<T, bool> isEnd, Func<T, IEnumerable<T>> getNext) where T : notnull
        {
            var queue = new Queue<T>();
            var visited = new Dictionary<T, int>();
            var parents = new Dictionary<T, T>();

            queue.Enqueue(startPoint);
            visited.Add(startPoint, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (isEnd(current))
                {
                    return (ReconstructPath(parents, current), visited[current]);
                }

                foreach (var next in getNext(current))
                {
                    if (visited.ContainsKey(next)) continue;

                    queue.Enqueue(next);
                    visited.Add(next, visited[current] + 1);
                    parents.Add(next, current);
                }
            }

            return (null, -1);
        }
    }
}
