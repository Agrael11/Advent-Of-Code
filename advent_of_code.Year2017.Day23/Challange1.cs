using System.Runtime.CompilerServices;

namespace advent_of_code.Year2017.Day23
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var multiplications = 0;

            var cpu = new CPU
            {
                MulCalled = () => multiplications++
            };
            cpu.Run(input);

            return multiplications;
        }
    }
}