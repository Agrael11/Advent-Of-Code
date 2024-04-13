namespace advent_of_code.Year2017.Day02
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var checksum = 0;

            foreach (var row in input)
            {
                var min = int.MaxValue;
                var max = int.MinValue;
                foreach (var cell in row.Split('\t'))
                {
                    var num = int.Parse(cell);
                    min = int.Min(min, num);
                    max = int.Max(max, num);
                }
                checksum += max - min;
            }

            return checksum;
        }
    }
}