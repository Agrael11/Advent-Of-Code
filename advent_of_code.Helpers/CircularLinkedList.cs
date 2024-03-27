using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public class CircularLinkedList<T> where T : notnull
    {
        private CircularListItem<T>? root;
        public int Length { get; private set; }
        private readonly Dictionary<T, CircularListItem<T>> list;


        public  CircularLinkedList()
        {
            root = null;
            list = new Dictionary<T, CircularListItem<T>>();
            Length = 0;
        }

        public T GetRoot()
        {
            if (root is null) throw new Exception("Tried to get from empty list");
            return root.Item;
        }

        public void Add(T item)
        {
            var newItem = new CircularListItem<T>(item);
            if (Length > 0 && root is not null)
            {
                newItem.PreviousItem = root.PreviousItem;
                newItem.NextItem = root;
                newItem.PreviousItem.NextItem = newItem;
                newItem.NextItem.PreviousItem = newItem;
            }
            else
            {
                root = newItem;
            }
            list.Add(item, newItem);
            Length++;
        }

        public T GetAfter(T item)
        {
            return list[item].NextItem.Item;
        }

        public T GetBefore(T item)
        {
            return list[item].PreviousItem.Item;
        }

        public void Remove(T item)
        {
            if (Length == 0) throw new Exception("Tried to remove from empty list");
            
            list[item].PreviousItem.NextItem = list[item].NextItem;
            list[item].NextItem.PreviousItem = list[item].PreviousItem;
            if (root == list[item]) root = list[item].NextItem;
            list.Remove(item);
            Length--;
        }
    }
}
