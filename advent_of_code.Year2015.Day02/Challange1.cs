namespace advent_of_code.Year2015.Day02
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var totalPaper = 0;

            foreach (var line in input)
            {
                var boxDef = line.Split('x');
                var length = int.Parse(boxDef[0]);
                var width = int.Parse(boxDef[1]);
                var height = int.Parse(boxDef[2]);
                List<int> sides = [length, width, height];
                sides.Sort();
                totalPaper += (2 * length * width) + (2 * width * height) + (2 * height * length) + (sides[0] * sides[1]);
            }

            return totalPaper;
        }
    }
}
