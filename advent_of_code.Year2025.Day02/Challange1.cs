using System.Text;
using Visualizers;

namespace advent_of_code.Year2025.Day02
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","").Split(",");
            var ranges = input.Select(item => {
                var split = item.Split('-');
                return (long.Parse(split[0]), long.Parse(split[1]));
                }).ToArray();

            var stringBuilder = new StringBuilder();
            if (AOConsole.Enabled) stringBuilder.AppendLine("Starting Part 1...");

            var sum = 0L;

            //We just check every number in every range for repeat - and add it to sum if it is.
            foreach (var (Start, End) in ranges)
            {
                for (var id = Start; id <= End; id++)
                {
                    if (IsRepeatingTwice(id))
                    {
                        sum += id;
                        if (AOConsole.Enabled) stringBuilder.AppendLine($"Adding {id}. Total is {sum}.");
                    }
                }
            }

            if (AOConsole.Enabled) AOConsole.WriteLine(stringBuilder.ToString());

            return sum;
        }

        /// <summary>
        /// Checks if the number is repeated twice
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns></returns>
        private static bool IsRepeatingTwice(long number)
        {
            //We get number of digits and from that.
            //If number doesn't have even number of digits it cannot repeat twice
            var length = Common.CountDigits(number);
            if (length == 0 || length % 2 == 1) return false;
            
            //Now we get the nearest higher power of 10 to half of the number
            var half = (length) / 2;
            var multiplier = (int)Math.Pow(10,half);
            
            //And if first half (division by multiplier) equals second half (modulo) it is repeating.
            return number / multiplier == number % multiplier;
        }
    }
}