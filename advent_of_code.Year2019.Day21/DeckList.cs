using System.Collections;

namespace advent_of_code.Year2019.Day22
{
    internal class DeckList : IEnumerable<long>
    {
        private readonly List<long> _list;
        private int _offset = 0;

        public DeckList(int size)
        {
            _list = new List<long>(size);
            for (long i = 0; i < size; i++)
            {
                _list.Add(i);
            }
        }

        public void Cut(int n)
        {
            _offset = (_offset + n + _list.Count) % _list.Count;
        }


        public IEnumerator<long> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
