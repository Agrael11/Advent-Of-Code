namespace advent_of_code.Year2019.Day18
{
    public static class Challange1
    {

        public static string DoChallange(string inputData)
        {
            //inputData = "#########\r\n#b.A.@.a#\r\n#########";
            //inputData = "########################\r\n#@..............ac.GI.b#\r\n###d#e#f################\r\n###A#B#C################\r\n###g#h#i################\r\n########################";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var map = new Map(input[0].Length, input.Length);
            map.LoadFromInput(input);
            
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
            var start = map.StartPoints[0];
            var priorityQueue = new PriorityQueue<(Common.Position position, ulong keyMask, int keyCount, int steps), int>();
            priorityQueue.Enqueue(new(new (start.X, start.Y), 0, 0, 0), 0);
            var visitedSteps = new Dictionary<(Common.Position position, ulong keyMask), int>();

            while (priorityQueue.Count > 0)
            {
                var (position, currentKeyMask, currentKeyCount, currentSteps) = priorityQueue.Dequeue();

                if (visitedSteps.TryGetValue((position, currentKeyMask), out var recordedSteps) && (recordedSteps < currentSteps))
                {
                    continue;
                }

                if (currentKeyCount >= map.KeyCount)
                {
                    return currentSteps;
                }
                var hash = ((ulong)position.X << 48) | ((ulong)position.Y << 32) | currentKeyMask;
                if (!memo.TryGetValue(hash, out var resultList))
                {
                    resultList = Common.SearchForKeys(position.X, position.Y, currentKeyMask, map);
                    memo[hash] = resultList;
                }
                foreach (var (resultSteps, resultPositiont, resultKey) in resultList)
                {
                    var nextSteps = currentSteps + resultSteps;
                    var nextMask = currentKeyMask | resultKey;
                    var nextState = (resultPositiont, nextMask);
                    if (visitedSteps.TryGetValue(nextState, out var previousSteps) && (previousSteps <= nextSteps))
                    {
                        continue;
                    }
                    visitedSteps[nextState] = nextSteps;
                    priorityQueue.Enqueue((resultPositiont, nextMask, currentKeyCount + 1, nextSteps), nextSteps);
                }
            }
            return null;
        }
    }
}