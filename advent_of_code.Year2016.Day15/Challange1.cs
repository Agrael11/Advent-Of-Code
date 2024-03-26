namespace advent_of_code.Year2016.Day15
{
    public static class Challange1
    {
        public static List<(int count, int start)> disks = new List<(int count, int start)>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("#","").Replace(".", "").TrimEnd('\n').Split('\n');
            foreach (var line in input)
            {
                var diskInfo = line.Split(' ');
                var positionCount = int.Parse(diskInfo[3]);
                var startPosition = int.Parse(diskInfo[^1]);
                disks.Add((positionCount, startPosition));
            }

            var startTime = 0;
            while (!FallsThrough(startTime))
            {
                startTime++;
            }

            disks.Clear();
            return startTime;
        }

        public static bool FallsThrough(int time)
        {
            for (var i = 0; i < disks.Count; i++)
            {
                if (!Safe(time, i)) return false;
            }
            return true;
        }

        public static bool Safe(int time, int disk)
        {
            return ((time + disk + 1 + disks[disk].start) % disks[disk].count) == 0;
        }
    }
}