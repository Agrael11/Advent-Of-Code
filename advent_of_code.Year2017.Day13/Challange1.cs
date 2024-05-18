namespace advent_of_code.Year2017.Day13
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var totalSeverity = 0;

            foreach (var layer in input)
            {
                var split = layer.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var layerNumber = int.Parse(split[0]);
                var layerDepth = int.Parse(split[1]);
                if (layerNumber % ((layerDepth * 2) - 2) == 0)
                {
                    totalSeverity += layerNumber*layerDepth;
                }
            }

            return totalSeverity;
        }
    }
}