using System.Text;
using Visualizers;

namespace advent_of_code.Year2019.Day24
{
    internal class Grid(int width, int height)
    {
        public ulong GridData { get; set; } = 0;
        public int Width { get; } = width;
        public int Height { get; } = height;

        public int CountBugs()
        {
            var count = 0;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    count += this[x, y]?1:0;
                }
            }
            return count;
        }

        public string Draw()
        {
            var builder = new StringBuilder();

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    builder.Append(this[x, y] ? '#': '.');
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public void SetFor(ref ulong otherData, int x, int y, bool state)
        {
            var bitPosition = y * Width + x;
            if (state)
            {
                otherData |= (1UL << bitPosition);
            }
            else
            {
                otherData &= ~(1UL << bitPosition);
            }
        }

        public bool this[int x, int y]
        {
            get
            {
                var bitPosition = y * Width + x;
                return (GridData & (1UL << bitPosition)) != 0;
            }
            set
            {
                var bitPosition = y * Width + x;
                if (value)
                {
                    GridData |= (1UL << bitPosition);
                }
                else
                {
                    GridData &= ~(1UL << bitPosition);
                }
            }
        }

        public static Grid Parse(string[] data)
        {
            var width = data[0].Length;
            var height = data.Length;
            var grid = new Grid(width, height);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = (data[y][x] == '#');
                }
            }
            return grid;
        }
    }
}