namespace advent_of_code.Year2019.Day19
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
               Split(',').Select(long.Parse).ToArray();

            var handler = new PCHandler(input);

            var searchedY = FindStartY(handler);
            var searchedX = GetLastRight(handler, 99, searchedY, 1);

            for (; ; )
            {
                searchedX = GetLastRight(handler, searchedX, searchedY, 1);
                if (IsSquare(handler, searchedX, searchedY, out var coordHash))
                {
                    return coordHash;
                }
                searchedY++;
            }
        }

        private static int FindStartY(PCHandler handler)
        {
            var y = 0;
            var last = handler.IsPointAffected(98, y);
            for (; ;)
            {
                var current = handler.IsPointAffected(98, y+1);
                if (!current && last)
                {
                    return y;
                }
                last = current;
                y++;
            }
        }

        private static bool IsSquare(PCHandler handler, int topX, int topY, out long coordHash)
        {
            var bottomX = topX - 99;
            var bottomY = topY + 99;
            coordHash = bottomX * 10000 + topY;
            if (bottomX < 0 || topY < 0 || !(handler.IsPointAffected(bottomX, bottomY))) return false;
            return true;
        }

        private static int GetLastRight(PCHandler handler, int x, int y, int step)
        {
            var lastRight = x;
            for (; ; )
            {
                var newState = handler.IsPointAffected(lastRight + step, y);
                if (!newState)
                {
                    return lastRight;
                }
                lastRight += step;
            }
        }
    }
}