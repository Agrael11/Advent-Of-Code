﻿namespace advent_of_code.Year2017.Day14
{
    public static class Challange2
    {
        private static readonly (int x, int y)[] NeighboursOffsets = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var used = new HashSet<(int x, int y)>();

            for (var y = 0; y < 128; y++)
            {
                var row = KnotHash.GetDenseHash(input + "-" + y, KnotHash.HashFormat.Binary);
                for (var x = 0; x < row.Length; x++)
                {
                    if (row[x] == '1') 
                    { 
                        used.Add((x, y));
                    }
                }
            }
            return CountGroups(used);
        }

        public static int CountGroups(HashSet<(int x, int y)> items)
        {
            var groups = 0;

            var stack = new Stack<(int x, int y)>();
            while (items.Count > 0)
            {
                stack.Push(items.First());
                groups++;

                while (stack.Count > 0)
                {
                    var current = stack.Pop();
                    foreach (var offset in NeighboursOffsets)
                    {
                        var x = current.x + offset.x;
                        var y = current.y + offset.y;
                        if (items.Contains((x,y)))
                        {
                            stack.Push((x, y));
                        }
                    }
                    items.Remove(current);
                }
            }

            return groups;
        }
    }
}