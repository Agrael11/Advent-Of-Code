using System.Text.RegularExpressions;

namespace advent_of_code.Year2018.Day13
{
    public static class Challange1
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
                input[y] = input[y].Replace('<', '-').Replace('>','-').Replace('^','|').Replace('v','|');
            }

            for (;;)
            {
                if (Common.MoveCarts(ref carts, ref input)) break;
            }

            var groups = carts.GroupBy(g => g.Position).ToList();
            var collidingGroups = groups.Where(g => g.Count() > 1).ToList();
            var orderedGroups = collidingGroups.OrderBy(g => g.Key.Y).ThenBy(g => g.Key.X).ToList();
            var (X, Y) = orderedGroups.First().Key;

            return $"{X},{Y}";
        }
    }
}