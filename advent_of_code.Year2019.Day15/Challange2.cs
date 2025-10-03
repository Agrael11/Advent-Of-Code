using IntMachine;
using System.Diagnostics;

namespace advent_of_code.Year2019.Day15
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",").Select(long.Parse).ToArray();

            var machine = new Machine()
            {
                RAM = (Memory)input
            };

            (var oxygenSource, var walls) = ExplorativeBFS(machine);

            return FloodFill(oxygenSource, walls);
        }

        private static int FloodFill((int x, int y) startLocation, (int x, int y)[] walls)
        {
            var directions = new (int dx, int dy)[]
                {
                    (0, 1),
                    (0, -1),
                    (-1, 0),
                    (1, 0)
                };

            var queue = new Queue<((int x, int y) location, int minutes)>();
            var visited = new HashSet<(int x, int y)>();
            var wallSet = walls.ToHashSet();
            var maxMinutes = 0;
            queue.Enqueue((startLocation, 0));
            while (queue.Count > 0)
            {
                var (currentLocation, minutes) = queue.Dequeue();
                if (!visited.Add(currentLocation))
                {
                    continue;
                }
                maxMinutes = Math.Max(maxMinutes, minutes);
               
                foreach (var (dx, dy) in directions)
                {
                    var nextLocation = (currentLocation.x + dx, currentLocation.y + dy);
                    if (wallSet.Contains(nextLocation))
                    {
                        continue;
                    }
                    queue.Enqueue((nextLocation, minutes + 1));
                }
            }
            return maxMinutes;
        }

        private static ((int x, int y) OxygenSource, (int x, int y)[] Walls) ExplorativeBFS(Machine machine)
        {
            var queue = new Queue<(Machine machine, (int x, int y) location)>();
            var visited = new HashSet<(int x, int y)>();
            var walls = new HashSet<(int x, int y)>();
            var oxygenSource = (0, 0);

            queue.Enqueue((machine, (0, 0)));

            while (queue.Count > 0)
            {
                var (currentMachine, currentLocation) = queue.Dequeue();
                
                for (var i = 1; i <= 4; i++)
                {
                    var nextLocation = Common.WalkCoordigate(currentLocation, i);
                    if (!visited.Add(nextLocation))
                    {
                        continue;
                    }

                    var nextMachine = currentMachine.Clone();
                    var output = Common.Navigate(nextMachine, i);
                    if (output == 0)
                    {
                        walls.Add(nextLocation);
                        continue;
                    }
                    if (output == 2)
                    {
                        oxygenSource = nextLocation;
                    }

                    queue.Enqueue((nextMachine, nextLocation));
                }
            }

            return (oxygenSource, walls.ToArray());
        }
    }
}