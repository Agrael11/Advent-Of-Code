namespace advent_of_code.Year2018.Day07
{
    internal class Worker(int id)
    {
        private readonly int timeLength = 61;
        public int WorkerID { get; private set; } = id;
        public long Time { get; private set; } = 0;
        public char? WorkingAt { get; set; } = null;

        public void SetWork(char node, long startTime)
        {
            Time = startTime + timeLength + (int)node - 'A';
            WorkingAt = node;
        }

        public void RemoveWork()
        {
            WorkingAt = null;
        }

    }
}
