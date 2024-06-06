namespace advent_of_code.Year2018.Day05
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var elements = new HashSet<char>();
            foreach (var letter in input)
            {
                if (letter >= 'a' && letter <= 'z') elements.Add(letter);
            }

            var lockObject = new Object();
            var lowest = int.MaxValue;

            foreach (var element in elements)
            {
                var list = GetWithoutElement(input, element);
                var result = Common.Compact(list);
                lock (lockObject)
                {
                    if (result < lowest) lowest = result;
                }
            }

            return lowest;
        }

        public static List<char> GetWithoutElement(string start, char element)
        {
            var list = start.ToList();
            list.RemoveAll((c) => (c == element) || (c == (element - 32)));
            return list;
        }
    }
}