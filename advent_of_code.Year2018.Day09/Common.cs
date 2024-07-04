namespace advent_of_code.Year2018.Day09
{
    internal static class Common
    {
        public static long SimulateGame(int numberOfPlayers, int lastMarble)
        {
            var playerScores = new long[numberOfPlayers];
            var maxScore = 0L;

            var currentMarble = new LinkedMarble(0);
            var currentPlayer = 0;

            for (var marbleValue = 1; marbleValue <= lastMarble; marbleValue++)
            {
                if (marbleValue % 23 == 0)
                {
                    currentMarble = currentMarble.SevenBefore;

                    playerScores[currentPlayer] += marbleValue + currentMarble.Value;
                    maxScore = long.Max(playerScores[currentPlayer], maxScore);

                    currentMarble = currentMarble.Next;
                    currentMarble.Remove();
                }
                else
                {
                    currentMarble = currentMarble.Next.AddBehind(marbleValue);
                }

                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            return maxScore;
        }
    }
}
