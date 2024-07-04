namespace advent_of_code.Year2018.Day08
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(' ');

            var data = new List<int>();
            foreach (var number in input)
            {
                data.Add(int.Parse(number));
            }

            return Node.Parse(ref data).MetadataSum;
        }
    }
}