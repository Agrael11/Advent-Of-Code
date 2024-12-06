namespace advent_of_code.Helpers
{
    public static class ExtensionsPermutations
    {
        public static List<List<T>> Permutate<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var items = source.ToList();
            var result = new List<List<T>>();

            void Generate(int index)
            {
                if (index == items.Count - 1)
                {
                    result.Add([.. items]);
                }
                else
                {
                    for (var i = index; i < items.Count; i++)
                    {
                        (items[index], items[i]) = (items[i], items[index]);
                        Generate(index + 1);
                        (items[index], items[i]) = (items[i], items[index]);
                    }
                }
            }

            Generate(0);
            return result;
        }

        public static IEnumerable<List<T>> YieldPermutate<T>(this IEnumerable<T> source)
        {
            var items = source.ToList();

            IEnumerable<IEnumerable<T>> Generate(int index)
            {
                if (index == items.Count - 1)
                {
                    yield return items;
                }
                else
                {
                    for (var i = index; i < items.Count; i++)
                    {
                        (items[index], items[i]) = (items[i], items[index]);

                        foreach (var permutation in Generate(index + 1))
                            yield return permutation;

                        (items[index], items[i]) = (items[i], items[index]);
                    }
                }
            }

            foreach (var v in Generate(0).Cast<List<T>>())
            {
                yield return v;
            }
        }
    }
}
