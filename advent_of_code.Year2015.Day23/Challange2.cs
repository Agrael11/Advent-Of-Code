namespace advent_of_code.Year2015.Day23
{
    public static class Challange2
    {
        public static uint DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var cpu = new CPU(input, 1);
            cpu.Run();

            return cpu.RegB;
        }
    }
}