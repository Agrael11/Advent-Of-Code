using advent_of_code.Helpers;
using Visualizers;

namespace advent_of_code.Year2018.Day23
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var nanobots = new List<(Vector3l position, int range)>();

            foreach (var line in input)
            {
                var split = line.Split(' ');
                var info = split[0][(split[0].IndexOf('<') + 1)..split[0].IndexOf('>')].Split(',').Select(long.Parse).ToArray();
                var range = int.Parse(split[1].Split('=')[1]);
                nanobots.Add((new Vector3l(info[0], info[1], info[2]), range));
            }


            var minX = nanobots.Min(t => t.position.X);
            var maxX = nanobots.Max(t => t.position.X);
            var minY = nanobots.Min(t => t.position.Y);
            var maxY = nanobots.Max(t => t.position.Y);
            var minZ = nanobots.Min(t => t.position.Z);
            var maxZ = nanobots.Max(t => t.position.Z);
            var mainBox = new Box(minX, minY, minZ, maxX, maxY, maxZ);

            var result = OctSearch(nanobots, mainBox);
            if (result.Count == 0) return -1;

            return result.Min(point=>Vector3l.ManhattanDistance(point,0,0,0));
        }

        private static List<Vector3l> OctSearch(List<(Vector3l position, int range)> nanobots, Box startBox)
        {
            var list = new List<Vector3l>();
            var foundLargest = 0;

            var priorityQueue = new PriorityQueue<(Box box, int range), int>();
            priorityQueue.Enqueue((startBox, nanobots.Count), 0);
            var checkedBoxes = 0;
            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();
                var boxes = current.box.Divide();
                if (checkedBoxes % 1000 == 0)
                {
                    AOConsole.Clear();
                    AOConsole.WriteLine($"Checked {checkedBoxes} boxes; biggest found ${foundLargest}\n" +
                        $"currentlyKnownBoxesToCheck {priorityQueue.Count}, Found positions {list.Count}");
                }
                if (current.range < foundLargest) continue;
                foreach (var box in boxes)
                {
                    checkedBoxes++;
                    var inRange = FindInRange(nanobots, box);
                    if (inRange >= foundLargest)
                    {
                        if (box.Width == 0 && box.Height == 0 && box.Depth == 0)
                        {
                            if (inRange > foundLargest)
                            {
                                list.Clear();
                                foundLargest = inRange;
                            }
                            list.Add(new Vector3l(box.X, box.Y, box.Z));
                            continue;
                        }
                        if (inRange > 0) priorityQueue.Enqueue((box, inRange), nanobots.Count-inRange);
                    }
                }
            }

            return list;
        }

        private static int FindInRange(List<(Vector3l position, int range)> nanobots, Box box)
        {
            var inrange = 0;
            foreach (var (position, range) in nanobots)
            {
                inrange += (box.Intersects(position, range) ? 1 : 0);
            }
            return inrange;
        }
    }
}