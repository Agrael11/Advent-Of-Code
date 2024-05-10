using System.Text;

namespace advent_of_code.Year2017.Day09
{
    public static class Challange2
    {
        public static readonly char[] AllowedCharacters = new char[] { '<', '>', '{', '}', '!' };

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var inGarbage = false;
            var garbage = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                switch (c)
                {
                    case '!':
                        i++;
                        break;
                    case '<':
                        if (inGarbage) garbage++;
                        else inGarbage = true;
                        break;
                    case '>':
                        inGarbage = false;
                        break;
                    default:
                        if (inGarbage) garbage++;
                        break;
                }
            }

            return garbage;
        }
    }
}