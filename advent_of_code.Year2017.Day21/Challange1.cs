using System.Net.NetworkInformation;

namespace advent_of_code.Year2017.Day21
{
    public static class Challange1
    {
        private static readonly int Cycles = 5;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            Common.ResetGrid();
            Common.ParseRules(input);
            
            var lights = 0L;
            for (var i = 0; i < Cycles; i++)
            {
                lights = Common.ExpandGrid();
            }

            return lights;
        }
    }
}