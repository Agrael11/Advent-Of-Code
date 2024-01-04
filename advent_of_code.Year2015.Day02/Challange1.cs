using System.Runtime.InteropServices.Marshalling;

namespace advent_of_code.Year2015.Day02
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            int totalPaper = 0;

            foreach (string line in input) 
            {
                string[] boxDef = line.Split('x');
                int length = int.Parse(boxDef[0]);
                int width = int.Parse(boxDef[1]);
                int height = int.Parse(boxDef[2]);
                List<int> sides = [length, width, height];
                sides.Sort();
                totalPaper += (2 * length * width) + (2 * width * height) + (2 * height * length) + (sides[0] * sides[1]);
            }

            return totalPaper;
        }
    }
}
