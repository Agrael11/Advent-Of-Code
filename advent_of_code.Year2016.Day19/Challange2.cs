using advent_of_code.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace advent_of_code.Year2016.Day19
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").Replace("\n", ""));

            var linkedList = new CircularLinkedList<int>();
            var targetElf = -1;

            for (var i = 0; i < input; i++)
            {
                linkedList.Add(i+1);
                if (i == input / 2) targetElf = i + 1;
            }
            while (targetElf != linkedList.GetCurrent())
            {
                linkedList.GetNext();
            }

            while (linkedList.Length > 1)
            {
                linkedList.RemoveCurrent();

                if (linkedList.Length % 2 == 0) linkedList.GetNext();
                else linkedList.GetCurrent();
            }

            return linkedList.GetRoot();
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