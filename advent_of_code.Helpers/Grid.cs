using System.Collections;

namespace advent_of_code.Helpers
{
    public class Grid<T>(int width, int height) : IEnumerable<T>
    {
        public int Width { get; private set; } = width;
        public int Height { get; private set; } = height;

        private readonly T[,] cells = new T[width, height];

        public T? this[int x, int y]
        {
            get => x < 0 || x >= Width || y < 0 || y >= Height ? default : cells[x, y];
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height) throw new IndexOutOfRangeException();
                if (value is null) throw new ArgumentNullException(nameof(value));
                cells[x, y] = value;
            }
        }

        public Grid<T> Clone()
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

        public DynamicGrid<T> ToDynamicGrid()
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

        public void RotateColumn(int columnIndex, int count)
        {
            var newColumnData = new T[Height];
            for (var i = 0; i < Height; i++)
            {
                var oldPosition = (Height + i - count) % Height;
                newColumnData[i] = cells[columnIndex, oldPosition];
            }
            for (var i = 0; i < Height; i++)
            {
                cells[columnIndex, i] = newColumnData[i];
            }
        }

        public void RotateRow(int rowIndex, int count)
        {
            var newRowData = new T[Width];
            for (var i = 0; i < Width; i++)
            {
                var oldPosition = (Width + i - count) % Width;
                newRowData[i] = cells[oldPosition, rowIndex];
            }
            for (var i = 0; i < Width; i++)
            {
                cells[i, rowIndex] = newRowData[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var cell in cells)
                yield return cell;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cells.GetEnumerator();
        }
    }
}
