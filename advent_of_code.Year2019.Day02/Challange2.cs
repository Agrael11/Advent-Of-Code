using System.ComponentModel.DataAnnotations;

namespace advent_of_code.Year2019.Day02
{
    public static class Challange2
    {
        private static readonly int Target = 19690720;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(",").Select(int.Parse).ToArray();

            for (var argument = 0; argument <= 9999; argument++)
            {
                var noun = argument / 100;
                var verb = argument % 100;
                if (Common.TryRunMachine(noun, verb, input, out var result) && result == Target)
                {
                    return 100 * noun + verb;
                }
            }
            
            return -1;
        }
    }
}