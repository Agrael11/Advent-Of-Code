namespace advent_of_code.Year2015.Day14
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var reindeers = new List<Reindeer>();
            foreach (var line in input)
            {
                reindeers.Add(new Reindeer(line));
            }

            var maxPosition = int.MinValue;
            foreach (var reindeer in reindeers)
            {
                maxPosition = int.Max(maxPosition, ReindeerPositionAt(reindeer, 2503));
            }

            return maxPosition;
        }

        private static int ReindeerPositionAt(Reindeer reindeer, int time)
        {
            var repeats = time / (reindeer.RestLength + reindeer.BurstLength);
            var timeTotal = repeats * (reindeer.RestLength + reindeer.BurstLength);
            var additionalDistance = int.Min((time - timeTotal) * reindeer.Speed, reindeer.Speed * reindeer.BurstLength);

            return repeats * reindeer.BurstLength * reindeer.Speed + additionalDistance;
        }
    }
}