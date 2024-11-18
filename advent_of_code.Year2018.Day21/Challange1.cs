using Visualizers;

namespace advent_of_code.Year2018.Day21
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var TargetRegister = int.Parse(input.Where(t => t.StartsWith("eqrr")).First().Split(' ')[1]);

            var cpu = new CPU(0);
            var result = 0;
            cpu.Registers.Register0Read += (object? sender, int e) =>
            {
                result = cpu.Registers[TargetRegister];
                cpu.Registers[0, true] = result;
            };
            cpu.Run(input);
            return result;
        }
    }
}