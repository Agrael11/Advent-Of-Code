namespace advent_of_code.Year2015.Day14
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var reindeers = new List<Reindeer>();
            foreach (var line in input)
            {
                reindeers.Add(new Reindeer(line));
            }

            for (var second = 0; second < 2503; second++)
            {
                foreach (var reindeer in reindeers)
                {
                    if (second % (reindeer.RestLength + reindeer.BurstLength) < reindeer.BurstLength)
                    {
                        reindeer.Distance += reindeer.Speed;
                    }
                }
                reindeers = [.. reindeers.OrderByDescending(t => t.Distance)];
                var best = reindeers[0].Distance;
                for (var i = 0; i < reindeers.Count; i++)
                {
                    if (reindeers[i].Distance != best) break;
                    reindeers[i].Points++;
                }
            }

            return reindeers.OrderByDescending(t => t.Points).First().Points;
        }
    }
}
