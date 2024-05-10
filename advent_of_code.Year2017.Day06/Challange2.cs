namespace advent_of_code.Year2017.Day06
{
    public static class Challange2
    {
        private static readonly char[] separator = new char[] { ' ', '\t' };
        private static readonly List<int> banks = new List<int>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            banks.Clear();

            foreach (var number in input)
            {
                banks.Add(int.Parse(number));
            }

            var known = new HashSet<string>();
            known.Add(string.Join(' ', banks));
            var target = "";

            var cycles = 1;

            for (; ; cycles++)
            {
                var i = SelectLargest();
                var toChange = banks[i];
                banks[i] = 0;
                i++;
                for (var offset = 0; offset < toChange; offset++)
                {
                    banks[(i + offset) % banks.Count]++;
                }
                var textRepresentation = string.Join(' ', banks);
                if (known.Contains(textRepresentation))
                {
                    target = textRepresentation;
                    break;
                }

                known.Add(textRepresentation);
            }

            cycles = 1;
            for (; ; cycles++)
            {
                var i = SelectLargest();
                var toChange = banks[i];
                banks[i] = 0;
                i++;
                for (var offset = 0; offset < toChange; offset++)
                {
                    banks[(i + offset) % banks.Count]++;
                }

                if (string.Join(' ', banks) == target)
                {
                    break;
                }
            }


            return cycles;
        }

        private static int SelectLargest()
        {
            var largestIndex = 0;
            var largest = int.MinValue;
            for (var i = 0; i < banks.Count; i++)
            {
                if (banks[i] > largest)
                {
                    largestIndex = i;
                    largest = banks[i];
                }
            }
            return largestIndex;
        }
    }
}