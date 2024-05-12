using System.Xml.XPath;

namespace advent_of_code.Year2017.Day11
{
    public static class Challange1
    {
        internal static Dictionary<string, (int xOffset, int yOffset)> Directions =
            new Dictionary<string, (int xOfffset, int yOffset)>()
            {
                {"n", (0, -2) },
                {"s", (0, 2) },
                {"w", (-2, 0) },
                {"e", (2, 0) },
                {"nw", (-1, -1) },
                {"ne", (1, -1) },
                {"sw", (-1, 1) },
                {"se", (1, 1) }
            };

        public static int DoChallange(string inputData)
        {
            var x = 0;
            var y = 0;

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(',');

            foreach (var direction in input)
            {
                x += Directions[direction].xOffset;
                y += Directions[direction].yOffset;
            }
            
            return GetDistance(x,y);
        }

        internal static int GetDistance(int x, int y)
        {
            return (Math.Abs(x) + Math.Abs(y)) / 2;
        }
    }
}