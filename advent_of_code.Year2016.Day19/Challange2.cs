using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace advent_of_code.Year2016.Day19
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").Replace("\n", ""));

            var firstElf = new Elf(1);
            var currentElf = firstElf;
            var targetElf = firstElf;

            for (var i = 1; i < input; i++)
            {
                currentElf.NextElf = new Elf(i+1)
                {
                    PreviousElf = currentElf
                };

                currentElf = currentElf.NextElf;

                if (i == input/2)
                {
                    targetElf = currentElf;
                }
            }
            firstElf.PreviousElf = currentElf;
            currentElf.NextElf = firstElf;
            currentElf = firstElf;

            while (currentElf.NextElf != currentElf)
            {
                targetElf.PreviousElf.NextElf = targetElf.NextElf;
                targetElf.NextElf.PreviousElf = targetElf.PreviousElf;
                targetElf = targetElf.NextElf;
                
                if (input % 2 == 1) targetElf = targetElf.NextElf;
                input--;

                currentElf = currentElf.NextElf;
            }

            return currentElf.ID;
        }

#pragma warning disable IDE0051 // Remove unused private members
        private static int JustANiceSolutionIDidNotComeUpWith(int input)
        {
            var count = (int)Math.Pow(3, Math.Floor(Math.Log(input, 3)));
            return input - count + int.Max(0, (input - 2 * count));
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}