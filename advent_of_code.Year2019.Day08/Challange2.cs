namespace advent_of_code.Year2019.Day08
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
                Select(t => byte.Parse(t.ToString())).ToArray();

            var image = new Image(25, 6, input);

            var realImage = image.RenderImage();
            
            Visualizers.AOConsole.WriteLine(realImage);

            return Helpers.AsciiArtReader.Reader.ReadText(realImage, 1, Helpers.AsciiArtReader.Reader.FontTypes.Type_A).Text;
        }
    }
}