namespace advent_of_code.Year2018.Day21
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var TargetRegister = int.Parse(input.Where(t => t.StartsWith("eqrr")).First().Split(' ')[1]);

            var last = -1;
            var cpu = new CPU(0);
            var result = 0;
            var seen = new HashSet<int>();
            cpu.Registers.Register0Read += (object? sender, int e) =>
            {
                var current = cpu.Registers[TargetRegister];
                if (!seen.Add(current))
                {
                    result = last;
                    cpu.Registers[0, true] = current;
                }
                last = current;
            };
            cpu.Run(input);
            return result;
        }
    }
}