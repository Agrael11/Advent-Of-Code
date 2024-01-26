namespace advent_of_code.Year2015.Day01
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');

            var level = 0;

            for (var i = 0; i < inputData.Length; i++)
            {
                var character = inputData[i];
                if (character == '(') level++;
                if (character == ')') level--;
                if (level < 0) return i;
            }
            return -1;
        }
    }
}
