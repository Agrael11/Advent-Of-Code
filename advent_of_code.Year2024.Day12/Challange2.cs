namespace advent_of_code.Year2024.Day12
{
    public static class Challange2
    {
        private readonly static HashSet<(int x, int y)> VisitedValues = new HashSet<(int x, int y)>();
        private readonly static (int offsetX, int offsetY)[] Offsets = [(-1, 0), (0, -1), (+1, 0), (0, +1)];
        private static int Width = 0;
        private static int Height = 0;

        public static long DoChallange(string inputData)
        {
            VisitedValues.Clear();
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(line => line.ToCharArray()).ToArray();
            Height = input.Length;
            Width = input.Length;

            var totalPrice = 0L;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (!VisitedValues.Contains((x, y)))
                    {
                        (var edges, var shape, var area) = GetShapeAndArea(x, y, input);
                        totalPrice += area * GetSides(edges, shape);
                    }
                }
            }

            return totalPrice;
        }

        //Gets number of sides
        private static int GetSides(List<(int x, int y)> edges, HashSet<(int x, int y)> shape)
        {
            var sidesV = GetSidesVertical(edges.OrderBy(p => p.y).ThenBy(p => p.x).ToList(), shape);
            var sidesH = GetSidesHorizontal(edges.OrderBy(p => p.x).ThenBy(p => p.y).ToList(), shape);
            return sidesV + sidesH;
        }

        //Now this one is mess
        private static int GetSidesVertical(List<(int x, int y)> edges, HashSet<(int x, int y)> shapeSet)
        {
            var sides = 0;
            var sideUp = false;
            var sideDown = false;
            var lastX = edges.First().x-1;
            var lastY = edges.First().y;

            /* This finds number of sides.
             * To explain simply, we check every point along the edge
             * If "lastX" is larger than 1 it means we had a gap and we end the current sides
             * If "lastY" is different than current we are on new column now - we also end the sides
             * If there is something above us and upper side is currently counted, we will end it there
             * If there is nothing above us and there is no currently active upper side, we start new one
             * Similarly for bottom side
             */

            foreach (var (x, y) in edges)
            {
                if (x - lastX > 1 || y != lastY)
                {
                    sideUp = false;
                    sideDown = false;
                }

                if (sideUp && shapeSet.Contains((x, y - 1)))
                {
                    sideUp = false;
                }
                if (!sideUp && !shapeSet.Contains((x, y - 1)))
                {
                    sideUp = true;
                    sides++;
                }
                if (sideDown && shapeSet.Contains((x, y + 1)))
                {
                    sideDown = false;
                }
                if (!sideDown && !shapeSet.Contains((x, y + 1)))
                {
                    sideDown = true;
                    sides++;
                }

                lastX = x;
                lastY = y;
            }

            return sides;
        }

        // This one is same as the one before, but with left and right sides
        private static int GetSidesHorizontal(List<(int x, int y)> edges, HashSet<(int x, int y)> shapeSet)
        {
            var sides = 0;
            var sideLeft = false;
            var sideRight = false;
            var lastY = edges.First().y - 1;
            var lastX = edges.First().x - 1;

            foreach (var (x, y) in edges)
            {
                if (x != lastX || y - lastY > 1)
                {
                    sideLeft = false;
                    sideRight = false;
                }

                if (sideLeft && shapeSet.Contains((x - 1, y)))
                {
                    sideLeft = false;
                }
                if (!sideLeft && !shapeSet.Contains((x - 1, y)))
                {
                    sideLeft = true;
                    sides++;
                }
                if (sideRight && shapeSet.Contains((x + 1, y)))
                {
                    sideRight = false;
                }
                if (!sideRight && !shapeSet.Contains((x + 1, y)))
                {
                    sideRight = true;
                    sides++;
                }

                lastX = x;
                lastY = y;
            }

            return sides;
        }

        //Similar to Part 1, but instead of perimeter we keep list of blocks in the shape
        private static (List<(int x, int y)> edges, HashSet<(int x, int y)> shape, long area) GetShapeAndArea(int xStartPoint, int yStartPoint, char[][] map)
        {
            var area = 0;
            var shape = new HashSet<(int x, int y)>();

            var positions = new Stack<(int x, int y)>();
            positions.Push((xStartPoint, yStartPoint));

            VisitedValues.Add((xStartPoint, yStartPoint));

            while (positions.Count > 0)
            {
                area++;
                var (x, y) = positions.Pop();
                var currentType = map[y][x];
                shape.Add((x, y));
                foreach (var (offsetX, offsetY) in Offsets)
                {
                    var nextX = x + offsetX;
                    var nextY = y + offsetY;
                    if (nextX < 0 || nextY < 0 || nextX >= Width || nextY >= Height || ((map[nextY][nextX]) != currentType))
                    {
                        continue;
                    }

                    if (VisitedValues.Add((nextX, nextY)))
                    {
                        positions.Push((nextX, nextY));
                    }
                }
            }

            //This finds edges of the shape for faster calculation
            var edges = new List<(int x, int y)>();
            foreach (var (x, y) in shape)
            {
                var inside = true;
                foreach (var (offsetX, offsetY) in Offsets)
                {
                    var nextX = x + offsetX;
                    var nextY = y + offsetY;
                    if (!shape.Contains((nextX, nextY))) inside = false;
                }
                if (!inside) edges.Add((x, y));
            }

            return (edges, shape, area);
        }
    }
}