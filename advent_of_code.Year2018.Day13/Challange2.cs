namespace advent_of_code.Year2018.Day13
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var carts = new List<Cart>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    Common.TryAddCart(ref carts, x, y, input);
                }
                input[y] = input[y].Replace('<', '-').Replace('>', '-').Replace('^', '|').Replace('v', '|');
            }

            for (; ; )
            {
                if (Common.MoveCarts(ref carts, ref input, true)) break;
            }

            var (X, Y) = carts.First().Position;

            return $"{X},{Y}";
        }
    }
}