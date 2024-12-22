namespace advent_of_code.Year2024.Day22
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "1\r\n10\r\n100\r\n2024";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(int.Parse).ToArray();

            //Just add up all 2000th secret numbers
            var total = 0L;
            foreach (var number in input)
            {
                total += CalculateSecretManyTimes(number, 2000);
            }

            return total;
        }

        private static long CalculateSecretManyTimes(long secret, int times)
        {
            //Will repeat 2000 times
            var monkey = new Monkey(secret);
            for (var i = 0; i < times; i++)
            {
                monkey.CalculateNextSecret();
            }
            return monkey.Secret;
        }
    }
}