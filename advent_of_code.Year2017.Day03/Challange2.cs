namespace advent_of_code.Year2017.Day03
{
    public static class Challange2
    {
        private static readonly Dictionary<(int x, int y), int> table = new Dictionary<(int x, int y), int>();

        public static int DoChallange(string inputData)
        {
            table.Clear();
            var input = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            table[(0, 0)] = 1;
            var lastValue = -1;
            for (var i = 1; lastValue < input ; i++)
            {
                lastValue = SetInTable(i);
            }

            return lastValue;
        }

        private static int SetInTable(int i)
        {
            var value = 0;
            (var originalX, var originalY) = ValueHelper.GetXY(i);
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;

                    var x = originalX + xOffset;
                    var y = originalY + yOffset;

                    if (table.TryGetValue((x, y), out var inTableValue))
                    {
                        value += inTableValue;
                    }
                }
            }
            table[(originalX, originalY)] = value;
            return value;
        }
    }
}