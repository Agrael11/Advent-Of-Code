namespace advent_of_code.Year2015.Day23
{
    public static class Challange1
    {
        public static uint DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var cpu = new CPU(input);
            cpu.Run();

            return cpu.RegB;
        }
    }
}