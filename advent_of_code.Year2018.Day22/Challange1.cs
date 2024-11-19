namespace advent_of_code.Year2018.Day22
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var Depth = int.Parse(input[0].Split(' ')[1]);
            (var X, var Y) = (int.Parse(input[1].Split(' ')[1].Split(',')[0]), int.Parse(input[1].Split(' ')[1].Split(',')[1]));

            var total = 0;
            var map = new Map(X, Y, Depth);

            for (var y = 0; y <= Y; y++)
            {
                for (var x = 0; x <= X; x++)
                {
                    total += (int)map.GetRegionTypeAt(x, y);
                }
            }

            return total;
        }
    }
}