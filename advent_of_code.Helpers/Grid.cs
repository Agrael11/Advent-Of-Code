namespace advent_of_code.Helpers
{
    public class Grid<T>(int width, int height)
    {
        public int Width { get; private set; } = width;
        public int Height { get; private set; } = height;

        private readonly T[,] cells = new T[width, height];

        public T? this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height) return default;
                return cells[x,y];
            }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height) throw new IndexOutOfRangeException();
                if (value is null) throw new ArgumentNullException(nameof(value));
                cells[x, y] = value;
            }
        }

        public Grid<T> Clone()
        {
            Grid<T> newGrid = new(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    newGrid[x, y] = this[x, y];
                }
            }
            return newGrid;
        }

        public DynamicGrid<T> ToDynamicGrid()
        {
            DynamicGrid<T> newGrid = new(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    newGrid[x, y] = this[x, y];
                }
            }
            return newGrid;
        }
    }
}
