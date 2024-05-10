using System.Text;

namespace advent_of_code.Year2017.Day09
{
    public static class Challange1
    {
        public static readonly char[] AllowedCharacters = new char[] { '<', '>', '{', '}', '!'};

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var builder = new StringBuilder();
            var inGarbage = false;

            for (var i = 0; i < input.Length; i++) 
            {
                var c = input[i];
                if (AllowedCharacters.Contains(c))
                {
                    switch (c)
                    {
                        case '!':
                            i++;
                            break;
                        case '<':
                            inGarbage = true;
                            break;
                        case '>':
                            inGarbage = false;
                            break;
                        default:
                            if (!inGarbage) builder.Append(c);
                            break;
                    }
                }
            }

            var total = 0;
            var level = 0;

            foreach (var c in builder.ToString())
            {
                if (c == '{')
                {
                    level++;
                    total += level;
                }
                if (c == '}')
                {
                    level--;
                }
            }

            return total;
        }
    }
}