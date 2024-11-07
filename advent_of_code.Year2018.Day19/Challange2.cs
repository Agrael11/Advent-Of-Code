using System.ComponentModel;

namespace advent_of_code.Year2018.Day19
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            
            var cpu = new CPU(1);
            cpu.Run(input);

            return cpu.GetRegister(0);
        }
    }
}