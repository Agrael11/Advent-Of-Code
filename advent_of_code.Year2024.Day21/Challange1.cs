using Visualizers;

namespace advent_of_code.Year2024.Day21
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            Common.RegisterButtons();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            /*var a = CrackTheCode("029A", 3);
            var b = CrackTheCode("980A", 3);
            var c = CrackTheCode("179A", 3);
            var d = CrackTheCode("456A", 3);
            var e = CrackTheCode("379A", 3);*/

            var total = 0L;
            foreach (var line in input)
            {
                total += Common.CrackTheCode(line, 3);
            }
            return total;
        }

        
    }
}