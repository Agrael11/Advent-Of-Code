namespace advent_of_code.Year2015.Day14
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<Reindeer> reindeers = [];
            foreach (string line in input)
            {
                reindeers.Add(new(line));
            }

            for (int second = 0; second < 2503; second++)
            {
                foreach (Reindeer reindeer in reindeers)
                {
                    if (second % (reindeer.RestLength + reindeer.BurstLength) < reindeer.BurstLength)
                    {
                        reindeer.Distance += reindeer.Speed;
                    }
                }
                reindeers = [..reindeers.OrderByDescending(t => { return t.Distance; })];
                int best = reindeers[0].Distance;
                for (int i = 0; i < reindeers.Count; i++)
                {
                    if (reindeers[i].Distance != best) break;
                    reindeers[i].Points++;
                }
            }

            return reindeers.OrderByDescending(t => { return t.Points; }).First().Points;
        }
    }
}
