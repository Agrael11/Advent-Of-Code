namespace advent_of_code.Year2017.Day14
{
    public static class Challange2
    {
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

            var queue = new Queue<(int x, int y)>();
            while (items.Count > 0)
            {
                queue.Enqueue(items.First());
                groups++;

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    for (var xOff = -1; xOff <= 1; xOff++)
                    {
                        for (var yOff = -1; yOff <= 1; yOff++)
                        {
                            if ((xOff == 0 && yOff == 0) || (xOff != 0 && yOff != 0)) continue;
                            var x = current.x + xOff;
                            var y = current.y + yOff;
                            if (items.Contains((x,y)))
                            {
                                queue.Enqueue((x, y));
                            }
                        }
                    }
                    items.Remove(current);
                }
            }

            return groups;
        }
    }
}