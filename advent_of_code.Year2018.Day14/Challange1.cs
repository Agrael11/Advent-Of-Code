namespace advent_of_code.Year2018.Day14
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var target = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            Common.Reset();

            while (Common.RecipesCount < target + 10)
            {
                Common.AddRound();
            }

            return Common.GetRecipesAsString(target, 10);
        }
    }
}