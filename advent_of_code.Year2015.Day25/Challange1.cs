using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day25
{
    public static class Challange1
    {
        private static readonly long StartCode = 20151125;
        private static readonly long Multiplier = 252533;
        private static readonly long Divider = 33554393;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var row = int.Parse(input.Split(' ', StringSplitOptions.RemoveEmptyEntries)[15][..^1]);
            var column = int.Parse(input.Split(' ', StringSplitOptions.RemoveEmptyEntries)[17][..^1]);
            var index = GetIndex(row, column);

            var code = StartCode;

            for (var i = 1; i < index; i++)
            {
                code *= Multiplier;
                code %= Divider;
            }

            return code;
        }

        public static int GetIndex(int row, int column)
        {
            return 1 + MathHelpers.Sum(1, row - 1) + MathHelpers.Sum(row + 1, row + column - 1);
        }
    }
}