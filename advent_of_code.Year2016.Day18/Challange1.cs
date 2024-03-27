namespace advent_of_code.Year2016.Day18
{
    public static class Challange1
    {
        private static readonly int rowCount = 40;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","");
            var row = new bool[input.Length];
            var safeCount = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var trap = input[i] != '.';
                row[i] = trap;
                safeCount += trap ? 0 : 1;
            }

            for (var rowId = 1; rowId < rowCount; rowId++)
            {
                (row, var count) = GenerateNextRow(row);
                safeCount += count;
            }

            return safeCount;
        }

        public static (bool[] newRow, int safeCount) GenerateNextRow(bool[] row)
        {
            var result = new bool[row.Length];
            var traps = 0;

            for (var i = 0; i < row.Length; i++)
            {
                var trapState = (i > 0) && row[i - 1];
                trapState ^= (i < row.Length - 1) && row[i + 1];
                traps += trapState ? 0 : 1;
                result[i] = trapState;
            }

            return (result, traps);
        }
    }
}