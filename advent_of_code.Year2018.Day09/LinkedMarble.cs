namespace advent_of_code.Year2018.Day09
{
    internal class LinkedMarble
    {
        public int Value;
        public LinkedMarble Next;
        public LinkedMarble Previous;
        public LinkedMarble SevenBefore => Previous.Previous.Previous.Previous.Previous.Previous.Previous;
        public LinkedMarble(int value, LinkedMarble previous, LinkedMarble next)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }
        public LinkedMarble(int value)
        {
            Value = value;
            Next = this;
            Previous = this;
        }
        public void Remove()
        {
            Previous.Next = Next;
            Next.Previous = Previous;
        }
        public LinkedMarble AddBehind(int value)
        {
            var newMarble = new LinkedMarble(value, this, Next);
            Next.Previous = newMarble;
            Next = newMarble;
            return newMarble;
        }

        public override string ToString()
        {
            return $"{Previous.Value} {{{Value}}} {Next.Value}";
        }
    }
}
