namespace advent_of_code.Year2018.Day12
{
    public static class Challange1
    {
        private static readonly int Generations = 20;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Initialize(input);

            Common.DoForNumberOfGens(Generations);

            return Common.CountPlants();
        }
    }
}