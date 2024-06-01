namespace advent_of_code.Year2018.Day01
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var result = 0;

            foreach (var line in input)
            {
                result += int.Parse(line);
            }

            return result;
        }
    }
}