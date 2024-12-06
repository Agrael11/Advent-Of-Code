namespace advent_of_code.Helpers
{
    public class BatchJob<SingleType,ResultType> (Func<List<SingleType?>, ResultType> countJob) 
    {
        public ResultType Results => countJob(Jobs.Select(job => job.Result).ToList());
        public int Size => Jobs.Count;
        public List<SingleJob<SingleType>> Jobs { get; } = new List<SingleJob<SingleType>>();

        public void Run()
        {
            foreach (var job in Jobs)
            {
                job.Run();
            }
        }
    }
}
