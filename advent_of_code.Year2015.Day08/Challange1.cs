namespace advent_of_code.Year2015.Day08
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var total = 0;
            var memory = 0;

            foreach (var inputLine in input)
            {
                total += inputLine.Length;
                var modifiedLine = inputLine[1..^1].Replace("\\\"", "\"").Replace("\\\\", "/");
                var backslashes = modifiedLine.Count(characetr => characetr == '\\');
                memory += modifiedLine.Length - backslashes * 3;
            }

            return total - memory;
        }
    }
}
