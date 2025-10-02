using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day12
{
    internal class Moon
    {
        public Vector3l Position { get; set; }
        public Vector3l Velocity { get; set; } = new Vector3l(0,0,0);

        public Moon(string inputString)
        {
            var definition = inputString[1..^1].Replace(" ", "").Split(",").
                Select(t => t.Split('=')).Select(t => (t[0].ToLower(), long.Parse(t[1]))).ToDictionary();
            Position = new Vector3l(definition["x"], definition["y"], definition["z"]);
        }

        public override string ToString()
        {
            return $"pos=<{Position}>, vel <{Velocity}>";
        }
    }
}
