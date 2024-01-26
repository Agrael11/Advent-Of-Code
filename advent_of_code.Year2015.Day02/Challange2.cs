namespace advent_of_code.Year2015.Day02
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var totalRibbon = 0;

            foreach (var line in input)
            {
                var boxDef = line.Split('x');
                var length = int.Parse(boxDef[0]);
                var width = int.Parse(boxDef[1]);
                var height = int.Parse(boxDef[2]);
                List<int> sides = [length, width, height];
                sides.Sort();
                totalRibbon += (length * width * height) + (sides[0] * 2) + (sides[1] * 2);
            }

            return totalRibbon;
        }
    }
}
