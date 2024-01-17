namespace advent_of_code.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Permutate<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            List<T> items = source.ToList();

            IEnumerable<IEnumerable<T>> Generate(int index)
            {
                if (index == items.Count - 1)
                {
                    yield return items;
                }
                else
                {
                    for (int i = index; i < items.Count; i++)
                    {
                        (items[index], items[i]) = (items[i], items[index]);

                        foreach (var permutation in Generate(index + 1))
                            yield return permutation;

                        (items[index], items[i]) = (items[i], items[index]);
                    }
                }
            }

            return Generate(0);
        }
    }
}
