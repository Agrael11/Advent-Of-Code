namespace advent_of_code.Year2017.Day13
{
    public static class Challange2
    {
        private static readonly List<(int number, int cycle)> Layers = new List<(int, int)>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            Layers.Clear();

            foreach (var layer in input)
            {
                var split = layer.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var layerNumber = int.Parse(split[0]);
                var layerDepth = int.Parse(split[1]);
                Layers.Add((layerNumber, (layerDepth * 2) - 2));
            }

            var delay = 0;

            for (; Caugh(delay); delay++) {; }

            return delay;
        }

        public static bool Caugh(int delay)
        {
            foreach (var (number, cycle) in Layers)
            {
                if ((number + delay) % cycle == 0) return true;
            }
            return false;
        }
    }
}