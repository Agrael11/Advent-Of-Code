namespace advent_of_code.Year2015.Day08
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var total = 0;
            var memory = 0;

            foreach (var inputLine in input)
            {
                total += inputLine.Length;
                var modifiedLine = inputLine.Replace("\\", "\\\\").Replace("\"", "\\\"");
                memory += modifiedLine.Length + 2;
            }

            return memory - total;
        }
    }
}
