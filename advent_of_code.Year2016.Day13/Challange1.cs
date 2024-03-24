using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day13
{
    public static class Challange1
    {
        private static long favNum;
        private static (int x, int y) target = (31, 39);

        public static int DoChallange(string inputData)
        {
            favNum = long.Parse(inputData.Replace("\r", "").Replace(",", "").Replace(".", "").TrimEnd('\n'));

            var startPosition = (1, 1);

            return Helpers.PathFinding.DoAStar(startPosition, IsEnd, GetNext, Heuristic, 1).cost;
        }

        public static int Heuristic((int x, int y) current)
        {
            var vDist = Math.Abs(target.y - current.y);
            var hDist = Math.Abs(target.x - current.x);
            return (int)Math.Sqrt(vDist * vDist + hDist * hDist);
        }

        public static IEnumerable<((int x, int y) nextPosition, int cost)> GetNext((int x, int y) current)
        {
            for (var yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (var xOffset = -1; xOffset <= 1; xOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;
                    if (xOffset != 0 && yOffset != 0) continue;
                    var rx = current.x + xOffset;
                    var ry = current.y + yOffset;
                    if (rx < 0 || ry < 0) continue;
                    if (IsWall(rx, ry)) continue;
                    yield return ((rx, ry), 1);
                }
            }
        }

        public static bool IsEnd((int x, int y) position)
        {
            return position.x == target.x && position.y == target.y;
        }

        private static bool IsWall(int x, int y)
        {
            var math = ((x + (2 * y) + 3) * x) + ((y + 1) * y) + favNum;

            return MathHelpers.BitCount(math) % 2 == 1;
        }
    }
}