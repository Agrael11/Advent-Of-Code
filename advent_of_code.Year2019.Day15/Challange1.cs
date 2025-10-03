using IntMachine;

namespace advent_of_code.Year2019.Day15
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",").Select(long.Parse).ToArray();

            var machine = new Machine()
            {
                RAM = (Memory)input
            };

            var result = DoBFS(machine);

            return result;
        }

        private static int DoBFS(Machine machine)
        {
            var queue = new Queue<(Machine machine, (int x, int y) location, int length)>();
            var visited = new HashSet<(int x, int y)>();

            queue.Enqueue((machine, (0, 0), 0));

            while (queue.Count > 0)
            {
                var (currentMachine, currentLocation, currentLength) = queue.Dequeue();
                
                for (var i = 1; i <= 4; i++)
                {
                    var nextLocation = Common.WalkCoordinate(currentLocation, i);
                    if (!visited.Add(nextLocation))
                    {
                        continue;
                    }

                    var nextMachine = currentMachine.Clone();
                    var output = Common.Navigate(nextMachine, i);
                    if (output == 0)
                    {
                        continue;
                    }
                    if (output == 2)
                    {
                        return currentLength+1;
                    }

                    queue.Enqueue((nextMachine, nextLocation, currentLength+1));
                }
            }

            return 0;
        }
    }
}