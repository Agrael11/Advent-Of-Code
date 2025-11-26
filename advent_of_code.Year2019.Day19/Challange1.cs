namespace advent_of_code.Year2019.Day19
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
               Split(',').Select(long.Parse).ToArray();

            var handler = new PCHandler(input);

            long affectedPoints = 0;

            for (var y = 0; y < 50; y++)
            {
                for (var x = 0; x < 50; x++)
                {
                    if (handler.IsPointAffected(x, y))
                    {
                        affectedPoints++;
                    }
                }
            }

            return affectedPoints;
        }
    }
}