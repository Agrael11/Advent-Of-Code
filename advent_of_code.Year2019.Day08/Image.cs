namespace advent_of_code.Year2019.Day08
{
    internal class Image
    {
        public int Width { get; }
        public int Height { get; }
        private readonly byte[] ColorData;
        public int LayerSize => Width * Height;
        public int LayerCount => ColorData.Length / LayerSize;

        public Image(int width, int height, byte[] colorData)
        {
            Width = width;
            Height = height;
            ColorData = colorData;
        }

        private byte[] GetLayerData(int layer)
        {
            return ColorData[layer..(layer + LayerSize)];
        }

        private byte GetColorAt(int x, int y, int layer)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x), "X coordinate is out of bounds.");
            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y), "Y coordinate is out of bounds.");
            if (layer < 0 || layer >= LayerCount)
                throw new ArgumentOutOfRangeException(nameof(layer), "Layer index is out of bounds.");
            var index = layer * LayerSize + y * Width + x;
            return ColorData[index];
        }

        private byte GetColorAt(int x, int y)
        {
            byte color = 2;
            var layer = 0;
            while (color == 2)
            {
                color = GetColorAt(x, y, layer);
                layer++;
            }
            return color;
        }

        public IEnumerable<byte[]> GetLayers()
        {
            for (var i = 0; i < LayerCount; i++)
            {
                yield return GetLayerData(i * LayerSize);
            }
        }

        public byte[,] GetImage()
        {
            var image = new byte[Width,Height];
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    image[x, y] = GetColorAt(x, y);
                }
            }
            return image;
        }

        public string RenderImage()
        {
            var sb = new System.Text.StringBuilder();
            var image = GetImage();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var color = image[x, y];
                    sb.Append(color == 0 ? " " : "#");
                }
                if (y < Height-1) sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
