namespace advent_of_code.Year2019.Day14
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.ParseRecipes(input);

            return Common.CraftFuel(1);
        }
    }
}