namespace advent_of_code.Year2016.Day12
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var cpu = new CPU(0, 0, 0, 0, input);
            cpu.Run();

            return cpu.Registers[0];
        }
    }
}