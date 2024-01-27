namespace advent_of_code.Year2015.Day20
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            var house = 1;

            while (SumDivisors(house) * 11 < input)
            {
                house++;
            }

            return house;
        }

        private static int SumDivisors(int number)
        {
            var divisorsSum = 0;

            for (var i = 1; i <= (int)Math.Sqrt(number); i++)
            {
                var quotient = number / i;
                var remainder = number % i;
                if (remainder == 0)
                {
                    if (i != quotient && i <= 50) divisorsSum += quotient;
                    if (quotient <= 50) divisorsSum += i;
                }
            }

            return divisorsSum;
        }
    }
}