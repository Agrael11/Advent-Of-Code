using System.Text;

namespace advent_of_code.Year2016.Day16
{
    public static class Challange1
    {
        private static readonly int TargetLength = 272;

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("#", "").Replace(".", "").TrimEnd('\n');

            input = Extend(input);

            input = GenerateChecksum(input);

            return input;
        }

        public static string GenerateChecksum(string input)
        {
            var builder = new StringBuilder(input);

            while (builder.Length % 2 == 0)
            {
                for (var i = 0; i < builder.Length - 1; i++)
                {
                    if (builder[i] == builder[i + 1])
                    {
                        builder.Remove(i, 2);
                        builder.Insert(i, "1");
                    }
                    else
                    {
                        builder.Remove(i, 2);
                        builder.Insert(i, "0");
                    }
                }
            }

            return builder.ToString();
        }

        public static string Extend(string input)
        {
            var builder = new StringBuilder(input);
            while (builder.Length < TargetLength)
            {
                var reverse = string.Join("", builder.ToString().Reverse().Select(c => c == '1' ? '0' : '1'));
                builder.Append('0');
                builder.Append(reverse);
            }
            return builder.ToString()[0..TargetLength];
        }
    }
}