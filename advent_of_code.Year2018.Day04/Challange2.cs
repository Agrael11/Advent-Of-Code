namespace advent_of_code.Year2018.Day04
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var guards = Common.GetGuards(input);
            
            Guard? bestGuard = null;
            var mostSlept = -1;
            var mostSleptMinute = -1;
            foreach (var guard in guards)
            {
                var (minute, count) = guard.GetMostSleepedMinute();
                if (count > mostSlept)
                {
                    bestGuard = guard;
                    mostSlept = count;
                    mostSleptMinute = minute;
                }
            }

            if (bestGuard is null) return -1;

            return bestGuard.ID * mostSleptMinute;
        }
    }
}