using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day11
{
    public static class Challange2
    {
        private static readonly int MaxFellsInRow = 5;

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            Common.Setup(int.Parse(input), true);

            for (var i = 0; i < 50; i++)
            {
                var l = MathHelpers.LargestDivisor(i);
                Console.WriteLine(l);
            }

            (var highestX, var highestY, var highestSize, var highestPower) = (-1, -1, -1, long.MinValue);
            var fallsInRow = 0;
            for (var size = 1; size <= 300 && fallsInRow < MaxFellsInRow; size++)
            {
                (var x, var y, var power) = Common.GetHighestAtSize(size);
                if (power > highestPower)
                {
                    (highestX, highestY, highestSize, highestPower) = (x, y, size, power);

                    fallsInRow = 0;
                }
                else
                {
                    fallsInRow++;
                }
            }
            return $"{highestX},{highestY},{highestSize}";
        }
    }
}