namespace advent_of_code.Year2017.Day01
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            var capcha = (input[0] == input[input.Length / 2]) ? input[0] - '0' : 0;

            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[(i + input.Length/2)%input.Length]) capcha += input[i] - '0';
            }

            return capcha;
        }
    }
}