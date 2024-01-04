namespace advent_of_code.Year2015.Day03
{
    public static class Challange2
    {
        private static bool step = false;
        private static (int X, int Y) position = (0, 0);
        private static (int X, int Y) position2 = (0, 0);
        private static HashSet<(int X, int Y)> visited = [];

        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');

            position = (0, 0);
            visited = [];
            step = false;

            foreach (char character in inputData)
            {
                switch (character)
                {
                    case '>': Move(+1,  0); break;
                    case '<': Move(-1,  0); break;
                    case '^': Move( 0, +1); break;
                    case 'v': Move( 0, -1); break;
                }
            }

            return visited.Count;
        }

        public static void Move(int x, int y)
        {
            step = !step;
            if (step)
            {
                position2 = (position2.X + x, position2.Y + y);
                (int X, int Y) newPosition = (position2.X, position2.Y);

                visited.Add(newPosition);
            }
            else
            {
                position = (position.X + x, position.Y + y);
                (int X, int Y) newPosition = (position.X, position.Y);

                visited.Add(newPosition);
            }
        }
    }
}
