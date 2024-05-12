namespace advent_of_code.Year2017.Day11
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var x = 0;
            var y = 0;

            var distance = 0;

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(',');

            foreach (var direction in input)
            {
                x += Challange1.Directions[direction].xOffset;
                y += Challange1.Directions[direction].yOffset;
                distance = int.Max(distance, Challange1.GetDistance(x, y));
            }

            return distance;
        }
    }
}