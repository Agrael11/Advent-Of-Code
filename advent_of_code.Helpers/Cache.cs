namespace advent_of_code.Helpers
{
    public class Cache<T1, T2> where T1 : notnull
    {
        private readonly Dictionary<T1, T2> cacheData;

        public Cache() 
        {
            cacheData = new Dictionary<T1, T2>();
        }

        public T2 TryGetResult(T1 key, Func<T1, T2> function)
        {
            if (cacheData.TryGetValue(key, out var result))
            {
                return result;
            }

            result = function.Invoke(key);
            cacheData.Add(key, result);
            return result;
        }

        public void Clear()
        {
            cacheData.Clear();
        }
    }
}
