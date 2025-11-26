namespace advent_of_code.Year2019.Day18
{

    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var map = new Map(input[0].Length, input.Length);

            map.LoadFromInput(input);
            map.FixStartPoint();

            var result = FindBestPath(map);

            if (result.HasValue)
            {
                return result.Value.ToString();
            }

            return "No Answer";
        }

        internal static int? FindBestPath(Map map)
        {
            var memo = new Dictionary<ulong, List<(int steps, Common.Position position, ulong key)>>();
            var starts = map.StartPoints;
            var priorityQueue = new PriorityQueue<(QuadPosition positions, ulong keyMask, int keyCount, int steps),
                int>();
            priorityQueue.Enqueue((new (starts[0].X, starts[0].Y, starts[1].X, starts[1].Y, starts[2].X, starts[2].Y, starts[3].X, starts[3].Y), 0, 0, 0), 0);
            var visitedSteps = new Dictionary<(QuadPosition positions, ulong keyMask), int>();

            while (priorityQueue.Count > 0)
            {
                var (currentPositions, currentKeyMask, currentKeyCount, currentSteps) = priorityQueue.Dequeue();

                if (visitedSteps.TryGetValue((currentPositions, currentKeyMask), out var recordedSteps) && (recordedSteps < currentSteps))
                {
                    continue;
                }

                if (currentKeyCount >= map.KeyCount)
                {
                    return currentSteps;
                }
                for (var i = 0; i < 4; i++)
                {
                    var currentX = currentPositions[i].X;
                    var currentY = currentPositions[i].Y;

                    var hash = ((ulong)currentX << 48) | ((ulong)currentY << 32) | currentKeyMask;
                    if (!memo.TryGetValue(hash, out var resultList))
                    {
                        resultList = Common.SearchForKeys(currentX, currentY, currentKeyMask, map);
                        memo[hash] = resultList;
                    }
                    foreach (var (resultSteps, resultPosition, resultKey) in resultList)
                    {
                        var nextSteps = currentSteps + resultSteps;
                        var nextMask = currentKeyMask | resultKey;
                        var nextPositions = new QuadPosition(currentPositions);
                        nextPositions[i] = (resultPosition.X, resultPosition.Y);
                        var nextState = (nextPositions, nextMask);
                        if (visitedSteps.TryGetValue(nextState, out var previousSteps) && (previousSteps <= nextSteps))
                        {
                            continue;
                        }
                        visitedSteps[nextState] = nextSteps;
                        priorityQueue.Enqueue((nextPositions, nextMask, currentKeyCount + 1, nextSteps), nextSteps);
                    }
                }
            }
            return null;
        }
    }
}