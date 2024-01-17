namespace advent_of_code.Year2015.Day14
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<Reindeer> reindeers = [];
            foreach (string line in input)
            {
                reindeers.Add(new(line));
            }

            int maxPosition = int.MinValue;
            foreach (Reindeer reindeer in reindeers)
            {
                maxPosition = int.Max(maxPosition, ReindeerPositionAt(reindeer, 2503));
            }

            return maxPosition;
        }

        private static int ReindeerPositionAt(Reindeer reindeer, int time)
        {
            int repeats = time / (reindeer.RestLength + reindeer.BurstLength);
            int timeTotal = repeats * (reindeer.RestLength + reindeer.BurstLength);
            int additionalDistance = int.Min((time - timeTotal) * reindeer.Speed, reindeer.Speed * reindeer.BurstLength);

            return repeats * reindeer.BurstLength * reindeer.Speed + additionalDistance;
        }
    }
}