namespace advent_of_code.Year2015.Day03
{
    public static class Challange1
    {
        private static (int X, int Y) position = (0, 0);
        private static readonly HashSet<(int X, int Y)> visited = new HashSet<(int X, int Y)>();

        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');

            position = (0, 0);
            visited.Clear();

            foreach (var character in inputData)
            {
                switch (character)
                {
                    case '>': Move(+1, 0); break;
                    case '<': Move(-1, 0); break;
                    case '^': Move(0, +1); break;
                    case 'v': Move(0, -1); break;
                }
            }

            return visited.Count;
        }

        public static void Move(int x, int y)
        {
            position = (position.X + x, position.Y + y);
            var newPosition = (position.X, position.Y);

            visited.Add(newPosition);
        }
    }
}
