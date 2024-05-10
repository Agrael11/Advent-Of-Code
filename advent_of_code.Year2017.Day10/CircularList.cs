using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day10
{
    internal class CircularList
    {
        private List<int> numbers;

        public CircularList(List<int> numbers)
        {
            this.numbers = numbers;
        }

        public string GetDenseHash()
        {
            var hash = "";

            for (var i = 0; i < 256; i+=16)
            {
                var dense = 0;
                for (var j = 0; j < 16; j++)
                {
                    dense ^= numbers[i+j];
                }
                hash += dense.ToString("X2");
            }

            return hash.ToLower();
        }

        public int Checksum()
        {
            return numbers[0] * numbers[1];
        }

        public int GetAt(int index)
        {
            return numbers[index % numbers.Count];
        }

        public void SetAt(int index, int value)
        {
            numbers[index % numbers.Count] = value;
        }

        public int FixPointer(int index)
        {
            return index % numbers.Count;
        }

        public void Reverse(int index, int length)
        {
            for (var offset = 0; offset < length/2; offset++)
            {
                var tmp = GetAt(index + length - offset - 1);

                SetAt(index + length - offset - 1, GetAt(index + offset));
                SetAt(index + offset, tmp);
            }
        }

        public override string ToString()
        {
            return string.Join(", ", numbers);
        }
    }
}
