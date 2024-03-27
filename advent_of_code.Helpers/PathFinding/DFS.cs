using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        public static (T? type, int length) DoDFS<T>(T startPoint, Func<T, bool> isEnd, Func<T, IEnumerable<T>> getNext) where T : notnull
        {
            var stack = new Stack<T>();
            var visited = new Dictionary<T, int>();

            stack.Push(startPoint);
            visited.Add(startPoint, 0);
            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (isEnd(current))
                {
                    return (current, visited[current]);
                }

                foreach (var next in getNext(current))
                {
                    if (visited.ContainsKey(next)) continue;

                    stack.Push(next);
                    visited.Add(next, visited[current] + 1);
                }
            }

            return (default, -1);
        }

        public static (List<T>? path, int length) DoDFSWithPath<T>(T startPoint, Func<T, bool> isEnd, Func<T, IEnumerable<T>> getNext) where T : notnull
        {
            var stack = new Stack<T>();
            var visited = new Dictionary<T, int>();
            var parents = new Dictionary<T, T>();

            stack.Push(startPoint);
            visited.Add(startPoint, 0);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (isEnd(current))
                {
                    return (ReconstructPath(parents, current), visited[current]);
                }

                foreach (var next in getNext(current))
                {
                    if (visited.ContainsKey(next)) continue;

                    stack.Push(next);
                    visited.Add(next, visited[current] + 1);
                    parents.Add(next, current);
                }
            }

            return (null, -1);
        }
    }
}
