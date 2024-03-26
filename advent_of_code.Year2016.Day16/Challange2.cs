using System.Text;

namespace advent_of_code.Year2016.Day16
{
    public static class Challange2
    {
        private static readonly int TargetLength = 35651584;

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("#", "").Replace(".", "").TrimEnd('\n');

            input = Extend(input);

            input = GenerateChecksum(input);

            return input;
        }

        public static string GenerateChecksum(string input)
        {
            var checksum = input;

            while (checksum.Length % 2 == 0)
            {
                var newChecksum = new StringBuilder(checksum.Length / 2);
                for (var i = 0; i < checksum.Length - 1; i+=2)
                {
                    if (checksum[i] == checksum[i + 1])
                    {
                        newChecksum.Append('1');
                    }
                    else
                    {
                        newChecksum.Append('0');
                    }
                }
                checksum = newChecksum.ToString();
            }

            return checksum;
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