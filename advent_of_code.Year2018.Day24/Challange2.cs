using Visualizers;

namespace advent_of_code.Year2018.Day24
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            (var immuneGroups, var infectionGroups) = Common.ParseInput(input);

            var largestLosingBoost = 0;
            var smallestWinningBoost = -1;
            var boost = 0;

            AOConsole.Clear();

            var lastWin = -1;
            
            for (; ; )
            {
                var (winner, ResultForce) = Common.Simulate(immuneGroups, infectionGroups, boost);

                AOConsole.WriteLine($"Boost: {boost}\nWinner is {winner.ToString() ?? "None"}; Result = {ResultForce}");

                if (winner == UnitGroup.UnitTypes.Immunity)
                {
                    smallestWinningBoost = boost;
                    
                    lastWin = ResultForce;
                    boost = (largestLosingBoost + smallestWinningBoost) / 2;
                    if (boost == largestLosingBoost || boost == smallestWinningBoost) break;
                    continue;
                }

                largestLosingBoost = boost;
                if (smallestWinningBoost == -1)
                {
                    boost += 1000;
                    continue;
                }
                boost = (largestLosingBoost + smallestWinningBoost) / 2;
                if (boost == largestLosingBoost || boost == smallestWinningBoost) break;
                continue;
            }

            return lastWin;
        }
    }
}