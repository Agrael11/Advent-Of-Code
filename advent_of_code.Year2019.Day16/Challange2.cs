namespace advent_of_code.Year2019.Day16
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input= inputData.Replace("\r", "").Replace("\n", "")
                .Select(t => int.Parse(t.ToString())).ToArray();

            var skip = int.Parse(inputData[..7]);
            
            var data = PrepareData(input, skip);

            for (var i = 0; i < 100; i++)
            {
                Sum(ref data);
            }

            return string.Join("", data[0..8]);
        }

        private static int[] PrepareData(int[] input, int skip)
        {
            var result = new int[input.Length * 10000 - skip];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = input[(i + skip )% input.Length];
            }
            return result;
        }

        private static void Sum(ref int[] data)
        {
            for (var i = data.Length - 2; i >= 0; i--)
            {
                data[i] = (data[i] + data[i + 1]) % 10;
            }
        }
    }
}