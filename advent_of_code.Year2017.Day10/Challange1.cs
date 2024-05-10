namespace advent_of_code.Year2017.Day10
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var list = new CircularList(Enumerable.Range(0, 256).ToList());

            var skip = 0;
            var index = 0;

            foreach (var length in input.Split(','))
            {
                var lng = int.Parse(length.Trim());
                list.Reverse(index, lng);
                index += lng + skip;
                index = list.FixPointer(index);
                skip++;
            }

            return list.Checksum();
        }
    }
}