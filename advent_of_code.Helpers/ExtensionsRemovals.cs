namespace advent_of_code.Helpers
{
    public static class ExtensionsRemovals
    {
        public static int RemoveAllWhere<T>(this HashSet<T> set, Predicate<T> predicate)
        {
            var removed = 0;
            var lastRemoved = 0;
            do
            {
                lastRemoved = set.RemoveWhere(predicate);
                removed += lastRemoved;
            } while (lastRemoved != 0);
            return removed;
        }

        public static IEnumerable<T> RemoveAllWhereGeneral<T>(this IEnumerable<T> set, Predicate<T> predicate)
        {
            var lastRemoved = 0;
            var mutated = set;
            do
            {
                mutated = mutated.RemoveWhereGeneral(predicate);
            } while (lastRemoved != 0);
            return mutated;
        }

        public static IEnumerable<T> RemoveWhereGeneral<T>(this IEnumerable<T> set, Predicate<T> predicate)
        {
            return set.Where(t => (!predicate(t)));
        }
    }
}
