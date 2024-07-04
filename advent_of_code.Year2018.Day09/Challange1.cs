namespace advent_of_code.Year2018.Day09
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(' ');

            var players = int.Parse(input[0]);
            var maxMarble = int.Parse(input[6]);
            var playerScores = new long[players];
            var current = new LinkedMarble(0);
            var currentPlayer = 0;
            var maxScore = 0L;
            for (var i = 1; i <= maxMarble; i++)
            {
                if (i % 23 == 0)
                {                    
                    playerScores[currentPlayer] += i;
                    current = current.SevenBefore;
                    playerScores[currentPlayer] += current.Value;

                    maxScore = long.Max(playerScores[currentPlayer], maxScore);

                    current.Remove();
                    current = current.Next;
                }
                else
                {
                    current = current.Next.AddBehind(i);
                }
                currentPlayer = (currentPlayer+1)%players;
            }

            return maxScore;
        }
    }
}