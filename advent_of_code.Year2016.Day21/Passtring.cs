using System;

namespace advent_of_code.Year2016.Day21
{
    public class Passtring
    {
        private List<char> data;
        private int length = 0;
        private Dictionary<int, int> table;

        public Passtring(string input)
        {
            data = input.ToList();
            length = data.Count;
            table = new Dictionary<int, int>();
            for (var i = 0; i < length; i++)
            {
                var (rotated, newposition) = RotateIndexInternal(i);
                table[newposition] = rotated;
            }
        }

        public void Swap(int index1, int index2)
        {
            index1 %= length;
            index2 %= length;
            (data[index2], data[index1]) = (data[index1], data[index2]);
        }

        public void Swap(char character1, char character2)
        {
            for (var i = 0; i < length; i++)
            {
                if (data[i] == character1) data[i] = character2;
                else if (data[i] == character2) data[i] = character1;
            }
        }

        public void RotateLeft(int count)
        {
            count %= length;
            var backup = data.GetRange(0, count);
            data.RemoveRange(0, count);
            data.AddRange(backup);
        }

        public void RotateRight(int count)
        {
            count %= length;
            var backup = data.GetRange(length-count, count);
            data.RemoveRange(length-count, count);
            data.InsertRange(0, backup);
        }

        public void RotateIndex(char character)
        {
            var charIndex = 0;
            for (var i = 0; i < length; i++)
            {
                if (data[i] == character)
                {
                    charIndex = i;
                    break;
                }
            }
            var rotate = charIndex + 1;
            if (charIndex >= 4) rotate++;
            RotateRight(rotate);
        }
        public void UnrotateIndex(char character)
        {
            var charIndex = 0;
            for (var i = 0; i < length; i++)
            {
                if (data[i] == character)
                {
                    charIndex = i;
                    break;
                }
            }
            RotateLeft(table[charIndex]);
        }

        private (int rotated, int newposition) RotateIndexInternal(int index)
        {
            var rotate = index + 1;
            if (index >= 4) rotate++;
            return (rotate, (rotate + index) % length);
        }

        public void Reverse(int start, int end)
        {
            start %= length;
            end %= length;
            while (start < end)
            {
                var index1 = (start) % length;
                var index2 = (end) % length;
                (data[index1], data[index2]) = (data[index2], data[index1]);
                start++;
                end--;
            }
        }

        public void Move(int index1, int index2)
        {
            index1 %= length;
            index2 %= length;
            var tmp = data[index1];
            data.RemoveAt(index1);
            data.Insert(index2, tmp);
        }

        public override string ToString()
        {
            return string.Join("", data);
        }
    }
}
 