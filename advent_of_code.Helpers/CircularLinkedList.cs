namespace advent_of_code.Helpers
{
    public class CircularLinkedList<T> where T : notnull
    {
        private CircularListItem<T>? root;
        public int Length { get; private set; }
        private CircularListItem<T>? current;


        public  CircularLinkedList()
        {
            root = null;
            current = null;
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
            current = newItem;
            Length++;
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
            if (Length == 1)
            {
                current = null;
                root = null;
            }
            else
            {
                if (current is null) throw new Exception("Tried to remove from empty list");

                current.NextItem.PreviousItem = current.PreviousItem;
                current.PreviousItem.NextItem = current.NextItem;
                if (root == current) root = current.NextItem;
                current = current.NextItem;
            }
            Length--;
        }
    }
}
