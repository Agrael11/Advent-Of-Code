using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public class BatchJob<SingleType,ResultType> (Func<List<SingleType?>, ResultType> countJob) 
    {
        public ResultType Results => countJob(Jobs.Select(job => job.Result).ToList());
        public int Size => Jobs.Count;
        public List<SingleJob<SingleType>> Jobs { get; } = new List<SingleJob<SingleType>>();

        public static void RunMultipleParallelized(List<BatchJob<SingleType, ResultType>> batches, ParallelOptions? parallelOptions)
        {
            parallelOptions ??= new ParallelOptions();
            Parallel.ForEach(batches, batch => batch.Run());
        }

        public void RunParallelized(ParallelOptions? parallelOptions)
        {
            parallelOptions ??= new ParallelOptions();
            Parallel.ForEach(Jobs, job => job.Run());
        }

        public void Run()
        {
            foreach (var job in Jobs)
            {
                job.Run();
            }
        }
    }
}
