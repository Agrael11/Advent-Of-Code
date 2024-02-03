namespace advent_of_code.Year2016.Day01
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(',');
            var currentDir = 0;
            var horizontal = 0;
            var vertical = 0;
            foreach (var direction in input)
            {
                var dir = direction.Trim();
                if (dir[0] == 'L') currentDir--;
                else currentDir++;
                if (currentDir > 3) currentDir -= 4;
                else if (currentDir < 0) currentDir += 4;

                if (currentDir == 0) vertical += int.Parse(dir[1..]);
                else if (currentDir == 2) vertical -= int.Parse(dir[1..]);
                else if (currentDir == 1) horizontal += int.Parse(dir[1..]);
                else if (currentDir == 3) horizontal -= int.Parse(dir[1..]);
            }

            return Math.Abs(vertical)+Math.Abs(horizontal);
        }
    }
}