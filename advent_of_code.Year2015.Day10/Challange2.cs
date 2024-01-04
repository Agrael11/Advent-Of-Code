using System.Text;

namespace advent_of_code.Year2015.Day10
{
    public static class Challange2
    {
        private static string LookAndSay(string input)
        {
            StringBuilder stringBuilder = new(input.Length * 2);

            int count = 1;

            for (int inputIndex = 1; inputIndex < input.Length; inputIndex++)
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
            string input = inputData.Replace("\r", "").TrimEnd('\n');

            for (int i = 0; i < 50; i++)
            {
                input = LookAndSay(input);
            }

            return input.Length;
        }
    }
}
