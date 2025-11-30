namespace advent_of_code.Year2019.Day23
{
    public static partial class Challange2
    {
        public static class NAT
        {
            public static long X { get; set; } = -1;
            public static long Y { get; set; } = -1;
            public static long LastSent { get; set; } = -1;
            public static int Retries { get; set; } = 0;
            public static readonly int MaxRetries = 2;

            public static bool IsRetryLimit()
            {
                return Retries >= MaxRetries;
            }
            public static void ResetRetries()
            {
                Retries = 0;
            }
            public static void Retry()
            {
                Retries++;
            }
        }
    }
}