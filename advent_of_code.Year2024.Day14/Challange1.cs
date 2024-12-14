namespace advent_of_code.Year2024.Day14
{
    public static class Challange1
    {
        private static readonly int WIDTH = 101;
        private static readonly int HEIGHT = 103;
        private static readonly int SECONDS = 100;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var quadrants = new long[] { 0L, 0L, 0L, 0L };

            foreach (var robot in input.Select(line => new Robot(line)))
            {
                //This moves robot 100 times by his vector
                robot.Move(WIDTH, HEIGHT, SECONDS);

                //We find in which quadrant he is
                var quadrant = GetQuadrant(robot.LocationX, robot.LocationY);
                
                //If robot is in quadrant, we increase count of robots in it
                if (quadrant >= 0)
                {
                    quadrants[quadrant]++;
                }
            }
            
            var total = 1L;
            foreach (var quadrant in quadrants)
            {
                total *= quadrant;
            }

            return total;
        }

        private static int GetQuadrant(long X, long Y)
        {
            //This just checks in which quadrant robots iis
            var middleColumn = WIDTH / 2;
            var middleRow = HEIGHT / 2;

            if (Y < middleRow) //Top half
            {
                if (X < middleColumn) //Top Left quadrant
                {
                    return 0;
                }
                else if (X > middleColumn) //Top Right quadrant
                {
                    return 1;
                }
            }
            else if (Y > middleRow) //Bottom Half
            {
                if (X < middleColumn) //Bottom Left quadrant
                {
                    return 2;
                }
                else if (X > middleColumn) //Bottom Right quadrant
                {
                    return 3;
                }
            }

            return -1; //In between quadrands. center lines don't count...
        }
    }
}