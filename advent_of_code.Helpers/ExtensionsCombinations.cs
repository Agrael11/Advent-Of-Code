using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public static class ExtensionsCombinations
    {
        public static List<List<T>> Combinations<T>(this IEnumerable<T> source)
        {
            var items = source.ToList();
            var result = new List<List<T>>();

            void Generate(int index, int size, List<T> currentCombination)
            {
                if (size == 0)
                {
                    result.Add(currentCombination.ToList());
                }
                else if (index == items.Count)
                {
                    return;
                }
                else
                {
                    currentCombination.Add(items[index]);
                    Generate(index + 1, size - 1, currentCombination);
                    currentCombination.RemoveAt(currentCombination.Count - 1);

                    Generate(index + 1, size, currentCombination);
                }
            }

            for (var size = 1; size <= items.Count; size++)
            {
                Generate(0, size, new List<T>());
            }

            return result;
        }

        public static IEnumerable<List<T>> YieldCombinations<T>(this IEnumerable<T> source)
        {
            var items = source.ToList();

            IEnumerable<IEnumerable<T>> Generate(int index, int size)
            {
                if (size == 0)
                {
                    yield return Enumerable.Empty<T>();
                }
                else if (index == items.Count)
                {
                    yield break;
                }
                else
                {
                    foreach (var combination in Generate(index + 1, size - 1))
                    {
                        yield return combination.Prepend(items[index]);
                    }

                    foreach (var combination in Generate(index + 1, size))
                    {
                        yield return combination;
                    }
                }
            }

            for (var size = 1; size <= items.Count; size++)
            {
                foreach (var combination in Generate(0, size))
                {
                    yield return combination.ToList();
                }
            }
        }
    }
}
