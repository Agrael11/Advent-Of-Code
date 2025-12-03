namespace advent_of_code.Year2025.Day03
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //Using Linq to parse strings into array of digits
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(item => item.Select(ch => int.Parse(ch.ToString())).ToArray());

            var sum = 0L;

            //For each battery bank we find the first largest battery and second largest battery in pack
            //Then we combine them and add to sum
            foreach (var bank in input)
            {
                var firstHighest = bank.IndexOf_Highest(0, bank.Length - 1);
                var secondHighest = bank.IndexOf_Highest(firstHighest + 1, bank.Length);
                var value = bank[firstHighest] * 10 + bank[secondHighest];
                sum += value;
            }

            return sum;
        }
    }
}