namespace advent_of_code.Helpers
{
    public class DirectCircularLinkedList<T> where T : notnull
    {
        private CircularListItem<T>? root;
        public int Length { get; private set; }
        private readonly Dictionary<T, CircularListItem<T>> list;
        private CircularListItem<T>? current;


        public  DirectCircularLinkedList()
        {
            root = null;
            current = null;
            list = new Dictionary<T, CircularListItem<T>>();
            Length = 0;
        }

        public T GetRoot()
        {
            if (root is null) throw new Exception("Tried to get from empty list");
            return root.Item;
        }

        public T GetCurrent()
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            return current.Item;
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
            current = newItem;
            Length++;
        }

        public T GetAfter(T item)
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            return list[item].NextItem.Item;
        }

        public T GetBefore(T item)
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            return list[item].PreviousItem.Item;
        }

        public T GetPrevious()
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            current = current.PreviousItem;
            return current.Item;
        }

        public T GetNext()
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            current = current.NextItem;
            return current.Item;
        }

        public void AdvanceCounter()
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            current = current.NextItem;
        }

        public void RewindCounter()
        {
            if (current is null) throw new Exception("Tried to get from empty list");
            current = current.PreviousItem;
        }

        public void RemoveCurrent()
        {
            if (current is null) throw new Exception("Tried to remove from empty list");

            if (Length == 1)
            {
                current = null;
                root = null;
                list.Clear();
            }
            else
            {
                current.NextItem.PreviousItem = current.PreviousItem;
                current.PreviousItem.NextItem = current.NextItem;
                if (root == current) root = root.NextItem;
                list.Remove(current.Item);
            }
            Length--;
        }

        public void Remove(T item)
        {
            if (Length == 0) throw new Exception("Tried to remove from empty list");
            
            list[item].PreviousItem.NextItem = list[item].NextItem;
            list[item].NextItem.PreviousItem = list[item].PreviousItem;
            if (root == list[item]) root = list[item].NextItem;
            if (current == list[item]) current = list[item].NextItem;
            list.Remove(item);
            Length--;
        }
    }
}
