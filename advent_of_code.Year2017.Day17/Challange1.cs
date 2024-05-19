namespace advent_of_code.Year2017.Day17
{
    public static class Challange1
    {
        private static readonly int Count = 2017;

        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").TrimEnd('\n'));

            var numbers = new List<int>(Count);
            numbers.Add(0);
            var pos = 0;

            for (var i = 1; i <= Count; i++)
            {
                pos = (pos + input) % numbers.Count + 1;
                numbers.Insert(pos, i);
            }

            return numbers[pos+1];
        }
    }
}