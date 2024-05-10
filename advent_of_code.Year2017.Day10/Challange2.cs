namespace advent_of_code.Year2017.Day10
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var newInput = "";

            foreach (var c in input)
            {
                newInput += (int)c + ",";
            }

            input = newInput + "17,31,73,47,23";

            var list = new CircularList(Enumerable.Range(0, 256).ToList());

            var skip = 0;
            var index = 0;

            for (var loops = 0; loops < 64; loops++)
            {
                foreach (var length in input.Split(','))
                {
                    var lng = int.Parse(length.Trim());
                    list.Reverse(index, lng);
                    index += lng + skip;
                    index = list.FixPointer(index);
                    skip++;
                }
            }

            return list.GetDenseHash();
        }
    }
}