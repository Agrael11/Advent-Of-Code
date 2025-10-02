namespace advent_of_code.Year2019.Day08
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
                Select(t => byte.Parse(t.ToString())).ToArray();

            var image = new Image(25, 6, input);

            var minlayer = image.GetLayers().MinBy(t => t.Count(c => c == 0));

            return minlayer is not null ?minlayer.Count(t => t == 1) * minlayer.Count(t => t == 2)
                : throw new Exception("No layers found");
        }
    }
}