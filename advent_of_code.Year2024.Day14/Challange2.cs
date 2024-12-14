namespace advent_of_code.Year2024.Day14
{
    public static class Challange2
    {
        private static readonly int WIDTH = 101;
        private static readonly int HEIGHT = 103;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var robots = input.Select(line => new Robot(line)).ToList();
            var map = new bool[WIDTH, HEIGHT];
            var rowLengths = new int[HEIGHT];

            //Precompute count of robots in lines
            foreach (var robot in robots)
            {
                rowLengths[robot.LocationY]++;
            }

            //We cound seconds until we find correct robot
            for (var second = 1; ; second++)
            {
                map = new bool[WIDTH, HEIGHT];

                //First we move all of them and update map and rowcounts
                foreach (var robot in robots)
                {
                    rowLengths[robot.LocationY]--;

                    //Inlining this makes it MUCH faster than calling Robot.Move() :P
                    var x = (robot.LocationX + robot.VectorX) % WIDTH;
                    var y = (robot.LocationY + robot.VectorY) % HEIGHT;
                    if (x < 0) x += WIDTH;
                    if (y < 0) y += HEIGHT;
                    robot.LocationX = x;
                    robot.LocationY = y;
                    
                    map[x, y] = true;
                    rowLengths[y]++;
                }
                
                //Then we search for line of robots
                for (var y = 0; y < HEIGHT; y++)
                {
                    if (rowLengths[y] < 30) continue;

                    var lineLength = 0;
                    for (var x = 0L; x < WIDTH; x++)
                    {
                        if (map[x,y])
                        {
                            lineLength++;
                            if (lineLength == 30) return second; //30 robots in line should mark correct one.
                        }
                        else
                        {
                            lineLength = 0;
                        }
                    }
                }
            }
        }
    }
}