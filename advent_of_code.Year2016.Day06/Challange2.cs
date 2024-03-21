namespace advent_of_code.Year2016.Day06
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var corrected = "";

            for (var i = 0; i < input[0].Length; i++)
            {
                var chars = new Dictionary<char, int>();
                foreach (var message in input)
                {
                    chars.TryGetValue(message[i], out var count);
                    chars[message[i]] = count + 1;
                }
                corrected += chars.MinBy(t => t.Value).Key;
            }
            return corrected;
        }
    }
}