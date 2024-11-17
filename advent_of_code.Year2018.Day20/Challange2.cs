using Visualizers;

namespace advent_of_code.Year2018.Day20
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Replace("^", "").Replace("$", "");
            Common.Init();
            AOConsole.Clear();

            var map = Common.BuildNavMap(input);
            if (map.OptionsCount == 0)
            {
                var nonSequence = new Sequence(null);
                nonSequence.AddSequencePart(map);
                Common.TraverseNavMap(nonSequence, (0, 0));
            }
            else
            {
                Common.TraverseNavMap(map.GetOption(0), (0, 0));
            }

            var nodesQueue = new Queue<(int distance, (int X, int Y) nodePosition)>();
            var visitedNodes = new HashSet<(int X, int Y)>();
            nodesQueue.Enqueue((0, (0, 0)));
            var nodesThousandDoorsFarCount = 0;

            while (nodesQueue.Count > 0)
            {
                var (currentDistance, currentNodePosition) = nodesQueue.Dequeue();
                nodesThousandDoorsFarCount += (currentDistance >= 1000) ? 1 : 0;
                var currentNode = Common.nodes[currentNodePosition];
                foreach (var connected in currentNode.ConnectedNodes)
                {
                    if (visitedNodes.Add(connected))
                    {
                        nodesQueue.Enqueue((currentDistance + 1, connected));
                    }
                }
            }

            return nodesThousandDoorsFarCount;
        }
    }
}