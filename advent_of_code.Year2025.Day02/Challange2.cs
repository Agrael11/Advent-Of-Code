using System.Text;
using Visualizers;

namespace advent_of_code.Year2025.Day02
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",");
            var ranges = input.Select(item => {
                var split = item.Split('-');
                return (long.Parse(split[0]), long.Parse(split[1]));
            }).ToArray();


            var stringBuilder = new StringBuilder();
            if (AOConsole.Enabled) stringBuilder.AppendLine("Starting Part 2...");

            var sum = 0L;

            //Same as part 1
            foreach (var (Start, End) in ranges)
            {
                for (var id = Start; id <= End; id++)
                {
                    if (IsRepeating(id))
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
        /// Finds if number is repeating at any length
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns></returns>
        private static bool IsRepeating(long number)
        {
            //We get length and half of the number again
            var length = Common.CountDigits(number);
            if (length == 0) return false;
            var half = (length) / 2;

            //But now for every single possible length (starting at half point - cannot repeat if higher)
            //we check if number is repeating and return true if proven
            for (var i = half; i >= 1; i--)
            {
                if (length % i != 0) continue; //If not divisible by repeat pattern length it cannot be repeating
                if (IsRepeating(number, i)) return true;
            }

            return false;
        }

        /// <summary>
        /// Finds if Number is repeating at given repeat length
        /// Will not give right result if length of number is not divisible by given length
        /// </summary>
        /// <param name="number">Number to be checked</param>
        /// <param name="repeatLength">Length of repeat pattern</param>
        /// <returns></returns>
        private static bool IsRepeating(long number, int repeatLength)
        {
            //We get the nearest higher power of 10 of the repeat length
            var multiplier = (int)Math.Pow(10, repeatLength);

            //Then we get sample - what we'll be checking repeats for (and divide number by the multiplier to remove the sample from end)
            var sample = number % multiplier;
            number /= multiplier;

            //We are checking while there is anything to check
            while (number > 0)
            {
                //If sample does not match current end of number, the number is not repeating, other wise we continue checking
                if (number % multiplier != sample) return false;
                number /= multiplier;
            }
            return true;
        }
    }
}