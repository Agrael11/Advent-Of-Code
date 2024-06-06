namespace advent_of_code.Year2018.Day02
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var threes = 0;
            var twos = 0;

            var counter = new Dictionary<char, int>();

            foreach (var line in input)
            {
                counter.Clear();
                foreach (var ch in line)
                {
                    if (counter.TryGetValue(ch, out var count))
                    {
                        counter[ch] = ++count;
                    }
                    else
                    {
                        counter[ch] = 1;
                    }
                }
                if (counter.ContainsValue(3)) threes++;
                if (counter.ContainsValue(2)) twos++;
            }

            return threes*twos;
        }
    }
}