namespace advent_of_code.Year2024.Day12
{
    public static class Challange1
    {
        private readonly static HashSet<(int x, int y)> VisitedValues = new HashSet<(int x, int y)> ();
        private readonly static (int offsetX, int offsetY)[] Offsets = [(-1,0),(0,-1),(+1,0),(0,+1)];
        private static int Width = 0;
        private static int Height = 0;

        public static long DoChallange(string inputData)
        {
            VisitedValues.Clear();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(line => line.ToCharArray()).ToArray();
            
            Height = input.Length;
            Width = input.Length;

            //For each cell we calculate price of it's area
            var totalPrice = 0L;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    //That is if the cell is not part of already checked area
                    if (!VisitedValues.Contains((x, y)))
                    {
                        totalPrice += GetPrice(x, y, input);
                    }
                }
            }


            return totalPrice;
        }

        //Uses DFS to find single shapes - calculating their area and perimeter along the way
        public static long GetPrice(int xStartPoint, int yStartPoint, char[][] map)
        {
            var area = 0;
            var perimeter = 0;
            
            var positions = new Stack<(int x, int y)>();
            positions.Push((xStartPoint, yStartPoint));

            //And single VisitedValues so we don't check same shape twice
            VisitedValues.Add((xStartPoint, yStartPoint));

            while (positions.Count > 0)
            {
                area++;
                var (x, y) = positions.Pop();
                var currentType = map[y][x];
                foreach (var (offsetX, offsetY) in Offsets)
                {
                    var nextX = x + offsetX;
                    var nextY = y + offsetY;
                    var nextType = currentType;
                    if (nextX < 0 || nextY < 0 || nextX >= Width || nextY >= Height || ((nextType = map[nextY][nextX]) != currentType))
                    {
                        perimeter++;
                        continue;
                    }
                                        
                    if (VisitedValues.Add((nextX, nextY)))
                    {
                        positions.Push((nextX, nextY));
                    }
                }
            }

            return area * perimeter;
        }
    }
}