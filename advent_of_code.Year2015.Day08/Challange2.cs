namespace advent_of_code.Year2015.Day08
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            int total = 0;
            int memory = 0;

            foreach (string inputLine in input)
            {
                total += inputLine.Length;
                string modifiedLine = inputLine.Replace("\\", "\\\\").Replace("\"", "\\\"");
                memory += modifiedLine.Length+2;
            }

            return memory - total;
        }
    }
}
