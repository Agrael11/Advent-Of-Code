namespace advent_of_code.Year2024.Day17
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            //Just simple parsing. It runs CPU
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t => t.Split(' ').Last()).ToArray();

            var regA = int.Parse(input[0]);
            var regB = int.Parse(input[1]);
            var regC = int.Parse(input[2]);
            var program = input[4].Split(',').Select(int.Parse).ToList();

            var cpu = new CPU(regA, regB, regC, program);

            var result = new List<int>();

            cpu.OutputEvent += (object? sender, CPUOutputEventArgs e) => result.Add(e.Output);
            cpu.Run();

            return string.Join(',', result);
        }
    }
}