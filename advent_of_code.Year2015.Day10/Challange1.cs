using System.Text;

namespace advent_of_code.Year2015.Day10
{
    public static class Challange1
    {
        private static string LookAndSay(string input)
        {
            var stringBuilder = new StringBuilder(input.Length * 2);

            var count = 1;

            for (var inputIndex = 1; inputIndex < input.Length; inputIndex++)
            {
                if (input[inputIndex] == input[inputIndex - 1])
                {
                    count++;
                    continue;
                }
                stringBuilder.Append(count).Append(input[inputIndex - 1]);
                count = 1;
            }

            stringBuilder.Append(count).Append(input[^1]);

            return stringBuilder.ToString();
        }

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            for (var i = 0; i < 40; i++)
            {
                input = LookAndSay(input);
            }

            return input.Length;
        }
    }
}