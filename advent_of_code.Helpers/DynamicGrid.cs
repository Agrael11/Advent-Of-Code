using System.Collections;

namespace advent_of_code.Helpers
{
    public class DynamicGrid<T>(int width, int height) : IEnumerable<T>
    {
        private int _width = width;
        private int _height = height;
        public int Width
        {
            get => _width;
            set
            {
                if (value < _width) Reduce(value, _height, true);
                if (value > _width) Expand(value, _height);
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                if (value < _height) Reduce(_width, value, true);
                if (value > _height) Expand(_width, value);
            }
        }

        private readonly Dictionary<(int x, int y), T> cells = new Dictionary<(int x, int y), T>();

        public void Expand(int newWidth, int newHeight)
        {
            if (newWidth > _width)
            {
                _width = newWidth;
            }
            if (newHeight > _height)
            {
                _height = newHeight;
            }
        }

        public void Reduce(int newWidth, int newHeight, bool full)
        {
            if (full)
            {
                for (var y = newHeight; y < _height; y++)
                {
                    for (var x = newWidth; x < _width; x++)
                    {
                        cells.Remove((x, y));
                    }
                }
            }

            if (newWidth < _width)
            {
                _width = newWidth;
            }
            if (newHeight < _height)
            {
                _height = newHeight;
            }
        }

        public T? this[int x, int y]
        {
            get => x < 0 || x >= Width || y < 0 || y >= Height ? default : !cells.ContainsKey((x, y)) ? default : cells[(x, y)];
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                {
                    throw new IndexOutOfRangeException();
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!cells.TryAdd((x, y), value))
                {
                    cells[(x, y)] = value;
                }
            }
        }

        public DynamicGrid<T> Clone()
        {
            var newGrid = new DynamicGrid<T>(Width, Height);
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    newGrid[x, y] = this[x, y];
                }
            }
            return newGrid;
        }

        public Grid<T> ToGrid()
        {
            var newGrid = new Grid<T>(Width, Height);
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    newGrid[x, y] = this[x, y];
                }
            }
            return newGrid;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicGridEnumerator<T>(cells, _width, _height);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
