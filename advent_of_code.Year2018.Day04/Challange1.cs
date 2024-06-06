using System.Numerics;

namespace advent_of_code.Year2018.Day04
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var guards = Common.GetGuards(input);

            Guard? bestGuard = null;
            var mostSlept = -1;
            foreach (var guard in guards)
            {
                if (guard.TotalSleepTime > mostSlept)
                {
                    bestGuard = guard;
                    mostSlept = guard.TotalSleepTime;
                }
            }

            if (bestGuard is null) return -1;

            return bestGuard.ID * bestGuard.GetMostSleepedMinute().minute;
        }
    }
}