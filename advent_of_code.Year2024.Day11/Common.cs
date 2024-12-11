namespace advent_of_code.Year2024.Day11
{
    internal class Common
    {
        private static readonly ulong MULTIPLIER = 2024;
        private static readonly ulong ULONG_ONE = 1;

        public static long CountNewStonesOverBlink(int blinksCount, Dictionary<ulong, long> numbers)
        {
            //Counts how many stones have we added.
            var total = 0L;

            //For how many times we blink
            for (var blink = 0; blink < blinksCount; blink++)
            {
                //For each unique stones with same number, we count how many new stones will they create.
                var newNumbers = new Dictionary<ulong, long>();
                foreach ((var number, var repeats) in numbers)
                {
                    total += CountOneVariantOfStones(number, repeats, newNumbers);
                }
                numbers = newNumbers;
            }
            return total;
        }

        private static long CountOneVariantOfStones(ulong number, long repeats, Dictionary<ulong, long> numbers)
        {
            //If number of digits in number is even, we will split it in two.
            //That is one new addition for each repeat of this number
            var numberString = number.ToString();

            if (numberString.Length % 2 == 0)
            {
                var divisor = (ulong)Math.Pow(10, numberString.Length / 2);
                var num1 = number / divisor;
                var num2 = number % divisor;
                numbers[num1] = numbers.GetValueOrDefault(num1, 0) + repeats;
                numbers[num2] = numbers.GetValueOrDefault(num2, 0) + repeats;
                return repeats;
            }

            //If number is 0 we will convert it to 1. This is 0 new numbers
            if (number == 0)
            {
                numbers[ULONG_ONE] = numbers.GetValueOrDefault(ULONG_ONE, 0) + repeats;
                return 0;
            }

            //Otherwise we will multiply the number by 2024 - no additions
            number *= MULTIPLIER;
            numbers[number] = numbers.GetValueOrDefault(number, 0) + repeats;
            return 0;
        }
    }
}
