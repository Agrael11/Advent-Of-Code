namespace advent_of_code.Year2018.Day19
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var cpu = new CPU();
            cpu.Run(input);

            return cpu.GetRegister(0);
        }
    }
}