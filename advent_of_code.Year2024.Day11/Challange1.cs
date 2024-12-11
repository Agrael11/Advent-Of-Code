namespace advent_of_code.Year2024.Day11
{
    public static class Challange1
    {
        public static readonly int BLINKS = 25;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(" ").Select(ulong.Parse);

            var numbers = input.ToDictionary(number => number, _ => 1L);

            //Counts new stones added over 25 blinks period + original stone count
            return Common.CountNewStonesOverBlink(BLINKS, numbers) + input.Count();
        }
    }
}