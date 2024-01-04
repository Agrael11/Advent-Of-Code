namespace advent_of_code.Year2015.Day02
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            int totalRibbon = 0;

            foreach (string line in input)
            {
                string[] boxDef = line.Split('x');
                int length = int.Parse(boxDef[0]);
                int width = int.Parse(boxDef[1]);
                int height = int.Parse(boxDef[2]);
                List<int> sides = [length, width, height];
                sides.Sort();
                totalRibbon += (length * width * height) + (sides[0] * 2) + (sides[1] * 2);
            }

            return totalRibbon;
        }
    }
}
