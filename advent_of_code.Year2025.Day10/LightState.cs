namespace advent_of_code.Year2025.Day10
{

    /// <summary>
    /// State of lights in current run of BFS. Faster than using tuples
    /// </summary>
    internal class LightState(int lights, int buttons, int steps)
    {
        public int Lights { get; set; } = lights;
        public int Buttons { get; set; } = buttons;
        public int Steps = steps;
    }
}
