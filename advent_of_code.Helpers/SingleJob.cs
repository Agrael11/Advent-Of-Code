namespace advent_of_code.Helpers
{
    public class SingleJob<T> (Func<T> func)
    {
        public T? Result { get; private set; }
        public Func<T> Function { get; } = func;

        public void Run()
        {
            Result = Function.Invoke();
        }
    }
}
