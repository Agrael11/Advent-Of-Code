namespace advent_of_code.Helpers
{
    public class SingleJob<T> (Func<T> func)
    {
        public T? Result { get; private set; }
        public Func<T> Function { get; } = func;

        public static void RunParallelized(List<SingleJob<T>> jobs)
        {
            Parallel.ForEach(jobs, job => job.Run());
        }

        public static List<BatchJob<T, ResultType>> RunParallelized<ResultType>(List<SingleJob<T>> jobs, int batchSize, Func<List<T?>, ResultType> countFunction)
        {
            var batches = new List<BatchJob<T, ResultType>>();
            var batch = new BatchJob<T, ResultType>(countFunction);
            foreach (var job in jobs)
            {
                batch.Jobs.Add(job);

                if (batch.Size == batchSize)
                {
                    batches.Add(batch);
                    batch = new BatchJob<T, ResultType>(countFunction);
                }
            }

            if (batch.Size > 0) batches.Add(batch);
            
            BatchJob<T, ResultType>.RunMultipleParallelized(batches);
            
            return batches;
        }

        public void Run()
        {
            Result = Function.Invoke();
        }
    }
}
