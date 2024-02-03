namespace advent_of_code.Year2016.Day01
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(',');
            var currentDir = 0;
            var currentX = 0;
            var currentY = 0;
            var visited = new HashSet<(int, int)> ();
            visited.Add((currentX, currentY));
            foreach (var direction in input)
            {
                var dir = direction.Trim();
                if (dir[0] == 'L') currentDir--;
                else currentDir++;
                if (currentDir > 3) currentDir -= 4;
                else if (currentDir < 0) currentDir += 4;

                var dist = int.Parse(dir[1..]);
                
                for (var i = 0; i < dist; i++)
                {
                    if (currentDir == 0) currentX++;
                    else if (currentDir == 2) currentX--;
                    else if (currentDir == 1) currentY++;
                    else if (currentDir == 3) currentY--;

                    if (visited.Contains((currentX, currentY)))
                    {
                        return Math.Abs(currentX)+Math.Abs(currentY);
                    }
                    else
                    {
                        visited.Add((currentX, currentY));
                    }
                }
            }

            return -1;
        }
    }
}