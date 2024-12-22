namespace advent_of_code.Year2024.Day21
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            Common.RegisterButtons();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var total = 0L;
            foreach (var line in input)
            {
                total += Common.CrackTheCode(line, 26); //25 Robots + 1 non-robot I guess.
            }

            return total;
        }
    }
}