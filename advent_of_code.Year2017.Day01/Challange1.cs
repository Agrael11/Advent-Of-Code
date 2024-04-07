namespace advent_of_code.Year2017.Day01
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            var capcha = (input[0] == input[^1])?input[0]-'0':0;

            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i+1]) capcha += input[i]-'0';
            }

            return capcha;
        }
    }
}