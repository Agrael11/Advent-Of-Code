using System.Text.RegularExpressions;

namespace advent_of_code.Year2024.Day03
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            //We start with Multiplication Enabled
            var multiplicationEnabled = true;
            var result = 0L;

            //Does regex matches for (mul\(\d*,\d*\))|(do\(\))|(don't\(\))
            //Regex is explained in it's file
            //If it won't find anything, we'll simply return 0
            var matches = Regexes.RegexMulsDoDonts().Matches(input);
            if (matches is null || matches.Count == 0) return 0;

            foreach (Match match in matches)
            {
                //Group 2 is "do()" - if it is, we enable multiplication
                if (match.Groups[2].Success)
                {
                    multiplicationEnabled = true;
                    continue;
                }
                //Group 3 is "don't()" - if it is, we disable multiplication
                if (match.Groups[3].Success)
                {
                    multiplicationEnabled = false;
                    continue;
                }

                //If multiplication is disabled OR if we Group 1 - "mul(number,number)" - is not found (that is not possible at this point)
                //We move on to next match
                if (!multiplicationEnabled || !match.Groups[1].Success)
                {
                    continue;
                }

                //Same as in part 1. Get inside of parenthesis, split by ',', parse as numbers, multiply, add to result
                var text = match.Value;

                var numbers = text[(text.IndexOf('(') + 1)..^1].Split(',');
                result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
            }

            return result;
        }
    }
}