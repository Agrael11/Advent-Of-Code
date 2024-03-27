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
        public static (T? type, int length) DoDFSLongest<T>(T startPoint, Func<T, bool> isEnd, Func<T, IEnumerable<T>> getNext) where T : notnull
        {
            var stack = new Stack<(T item, int length)>();

            stack.Push((startPoint, 0));

            var longestDistance = 0;
            var longestItem = startPoint;

            while (stack.Count > 0)
            {
                var (item, length) = stack.Pop();

                if (isEnd(item))
                {
                    if (length > longestDistance)
                    {
                        longestDistance = length;
                        longestItem = item;
                    }
                    continue;
                }

                foreach (var next in getNext(item))
                {
                    stack.Push((next, length + 1));
                }
            }

            return (longestItem, longestDistance);
        }
    }
}
