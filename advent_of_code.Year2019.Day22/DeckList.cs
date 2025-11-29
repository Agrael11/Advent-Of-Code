using System.Collections;

namespace advent_of_code.Year2019.Day22
{
    internal class DeckList : IEnumerable<long>
    {
        private Dictionary<long, long> _list;
        private int _offset = 0;

        public long this[int index] => _list[(_offset + index) % _list.Count];

        public int IndexOf(long value)
        {
            for (var i = 0; i < _list.Count; i++)
            {
                if (_list[(_offset + i) % _list.Count] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public DeckList(long size)
        {
            _list = new Dictionary<long, long>();
            for (long i = 0; i < size; i++)
            {
                _list.Add(i, i);
            }
        }

        public DeckList(Dictionary<long, long> list)
        {
            _list = list;
        }

        public void Cut(int n)
        {
            _offset += n;
            while (_offset < 0)
            {
                _offset += _list.Count;
            }
            _offset %= _list.Count;
        }

        public void DealWithIncrement(int increment)
        {
            var newList = new Dictionary<long, long>();
            for (var i = 0; i < _list.Count; i++)
            {
                var value = _list[(_offset + i) % _list.Count];
                newList[(i * increment) % _list.Count] = value;
            }
            _list = newList;
            _offset = 0;
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            for (var i = 0; i < _list.Count; i++)
            {
                builder.Append(_list[(_offset + i) % _list.Count]);
                if (i < _list.Count - 1)
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }

        public void Deal()
        {
            var newList = new Dictionary<long, long>();
            for (var i = 0; i < _list.Count; i++)
            {
                var value = _list[(_offset + i) % _list.Count];
                newList[_list.Count - 1 - i] = value;
            }
            _list = newList;
            _offset = 0;
        }

        public IEnumerator<long> GetEnumerator()
        {
            for (var i = 0; i < _list.Count; i++)
            {
                yield return _list[(_offset + i) % _list.Count];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
