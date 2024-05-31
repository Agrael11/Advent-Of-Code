namespace advent_of_code.Year2017.Day22
{
    public static class Challange1
    {
        private static readonly HashSet<(int x,int y)> grid = new HashSet<(int x,int y)>();
        private static readonly int Bursts = 10000;

        public static int DoChallange(string inputData)
        {
            grid.Clear();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            (int x, int y) current = (input[0].Length/2, input.Length/2);
            var currentDirection = 0;

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#') grid.Add((x, y));
                }
            }

            var infections = 0;
            for (var i = 0; i < Bursts; i++)
            {
                if (grid.Contains(current))
                {
                    currentDirection = (currentDirection + 1) % 4;
                    grid.Remove(current);
                }
                else
                {
                    currentDirection = (currentDirection + 3) % 4;
                    infections++;
                    grid.Add(current);
                }

                switch (currentDirection)
                {
                    case 0:
                        current.y--;
                        break;
                    case 1:
                        current.x++;
                        break;
                    case 2:
                        current.y++;
                        break;
                    case 3:
                        current.x--;
                        break;
                }
            }
            
            return infections;
        }
    }
}