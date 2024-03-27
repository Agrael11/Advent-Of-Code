using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    internal class CircularListItem<T>
    {
        public T Item { get; set; }
        public CircularListItem<T> PreviousItem;
        public CircularListItem<T> NextItem;

        public CircularListItem(T item)
        {
            Item = item;
            PreviousItem = this;
            NextItem = this;
        }

        public override string ToString()
        {
            return $"{Item}, {{{PreviousItem.Item}-{NextItem.Item}}}";
        }
    }
}
