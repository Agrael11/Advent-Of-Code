namespace advent_of_code.Year2015.Day08
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            int total = 0;
            int memory = 0;

            foreach (string inputLine in input)
            {
                total += inputLine.Length;
                string modifiedLine = inputLine[1..^1].Replace("\\\"", "\"").Replace("\\\\","/");
                int backslashes = modifiedLine.Count(characetr => { return characetr == '\\'; });
                memory += modifiedLine.Length - backslashes * 3;
            }

            return total - memory;
        }
    }
}
