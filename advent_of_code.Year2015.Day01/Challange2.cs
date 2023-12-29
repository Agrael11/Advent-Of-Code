namespace advent_of_code.Year2015.Day01
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');
            int level = 0;
            for (int i = 0; i < inputData.Length; i++)
            {
                char c = inputData[i];
                if (c == '(') level++;
                if (c == ')') level--;
                if (level < 0) return i;
            }
            return -1;
        }
    }
}
