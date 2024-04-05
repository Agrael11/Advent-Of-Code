using System.Collections;

namespace advent_of_code.Helpers
{
    public class DynamicGridEnumerator<T>(Dictionary<(int x, int y), T> elements, int width, int height) : IEnumerator<T>
    {
        private Dictionary<(int x, int y), T> _elements = elements;
        private int _width = width;
        private int _height = height;
        private int currentIndex = -1;

        public T Current => _elements[(currentIndex%_width, currentIndex/_width)];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < _width * _height;
        }

        public void Reset()
        {
            currentIndex = -1;
        }
    }
}
