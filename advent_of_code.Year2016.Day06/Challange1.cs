namespace advent_of_code.Year2016.Day06
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var corrected = "";

            for (var i = 0; i < input[0].Length; i++)
            {
                var chars = new Dictionary<char, int>();
                var mostOccuringChar = '\0';
                var mostOccuringTimes = 0;
                foreach (var message in input)
                {
                    chars.TryGetValue(message[i], out var count);
                    chars[message[i]] = count + 1;
                    if (count + 1 > mostOccuringTimes)
                    {
                        mostOccuringTimes = count + 1;
                        mostOccuringChar = message[i];
                    }
                }
                corrected += mostOccuringChar;
            }
            return corrected;
        }
    }
}