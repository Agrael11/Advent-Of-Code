using System.Text.RegularExpressions;

namespace advent_of_code.Year2024.Day03
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var result = 0L;

            //Does regex matches for mul\(\d{1,3},\d{1,3}\)
            //Regex is explained in it's file
            //If it won't find anything, we'll simply return 0
            var matches = Regexes.RegexMuls().Matches(input);
            if (matches is null || matches.Count == 0) return 0;
            
            foreach (Match match in matches)
            {
                //If match is not success we ignore this line
                //(this is technically impossible lol. if match was found.. it has to be a success!)
                if (!match.Success)
                {
                    continue;
                }

                //We'll get text, get insides of parenthesis, split it by ','
                var text = match.Value;
                var numbers = text[(text.IndexOf('(') + 1)..^1].Split(',');
                //then we just multiply the numbers and add it to result.
                result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
            }

            return result;
        }
    }
}