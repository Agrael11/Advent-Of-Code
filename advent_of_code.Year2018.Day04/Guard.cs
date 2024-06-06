using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day04
{
    internal class Guard(int id)
    {
        public int ID { get; } = id;
        private readonly Dictionary<int, int> sleepCounts = new Dictionary<int, int>();
        public int TotalSleepTime { get; private set; } = 0;

        public void AddSleep(int start, int end)
        {
            TotalSleepTime += end - start;
            for (var i = start; i < end; i++)
            {
                if (!sleepCounts.TryGetValue(i, out var count))
                {
                    count = 0;
                }
                sleepCounts[i] = ++count;
            }
        }

        public (int minute, int count) GetMostSleepedMinute()
        {
            var mostSleeped = -1;
            var mostSleepedCount = -1;
            foreach (var (minute, count) in sleepCounts)
            {
                if (count > mostSleepedCount)
                {
                    mostSleeped = minute;
                    mostSleepedCount = count;
                }
            }

            return (mostSleeped, mostSleepedCount);
        }
    }
}
