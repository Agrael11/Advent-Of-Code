using advent_of_code.Helpers;
using Visualizers;

namespace advent_of_code.Year2018.Day23
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var nanobots = new List<(Vector3l position, int range)>();

            foreach (var line in input)
            {
                var split = line.Split(' ');
                var info = split[0][(split[0].IndexOf('<')+1)..split[0].IndexOf('>')].Split(',').Select(long.Parse).ToArray();
                var range = int.Parse(split[1].Split('=')[1]);
                nanobots.Add((new Vector3l(info[0], info[1], info[2]), range));
            }

            var strongest = nanobots.MaxBy(t => t.range);
            
            var inRange = 0;

            foreach (var (nanobotPosition, nanobotRange) in nanobots)
            {
                inRange += (strongest.range >= Vector3l.ManhattanDistance(strongest.position, nanobotPosition)) ? 1 : 0;
            }

            return inRange;
        }
    }
}