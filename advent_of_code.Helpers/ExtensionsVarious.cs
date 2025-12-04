namespace advent_of_code.Helpers
{
    public static class ExtensionsVarious
    {
        public static int RemoveAllWhere<T>(this HashSet<T> set, Predicate<T> match)
        {
            var removed = 0;
            var lastRemoved = 0;
            do
            {
                lastRemoved = set.RemoveWhere(match);
                removed += lastRemoved;
            } while (lastRemoved != 0);
            return removed;
        }
    }
}
