using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day06
{

    public static class Challange2
    {
        private static readonly int JOBCOUNT = 500;
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Parse(input);

            //We back up original guard position
            (var guardX, var guardY, var guardDirection) = Common.GuardState;

            //And find locations he would visit
            var visitedLocations = Common.GetVisitedLocations();
            visitedLocations.Remove((guardX, guardY));

            //Convert location list to list of loop detection jobs
            var jobs = visitedLocations.Select(location => new SingleJob<bool>(() => WouldBeLoop(guardX, guardY, guardDirection, location.X, location.Y))).ToList();

            //Run all jobs is parallel, constrained multiple times. we count only jobs that resulted in loop.
            var batches = SingleJob<bool>.RunParallelized<int>(jobs, JOBCOUNT, (result) => result.Count(v => v == true));
            
            return batches.Sum(batch => batch.Results);
        }

        public static bool WouldBeLoop(int X, int Y, int Direction, int extraX, int extraY)
        {
            var Visited = new HashSet<(int X, int Y, int Direction)>();
            for (; ; )
            {
                //If we already were at this state, we are in loop
                if (!Visited.Add((X, Y, Direction))) return true;

                //We find next state depending on our direction
                switch(Direction)
                {
                    case 0:
                        (X, Y) = GetLastSafeUp(X, Y, extraX, extraY);
                        break;
                    case 1:
                        (X, Y) = GetLastSafeRight(X, Y, extraX, extraY);
                        break;
                    case 2:
                        (X, Y) = GetLastSafeDown(X, Y, extraX, extraY);
                        break;
                    case 3:
                        (X, Y) = GetLastSafeLeft(X, Y, extraX, extraY);
                        break;
                }

                //If we'e out of bounds, we are NOT in loop
                if (X < 0 || Y < 0 || X >= Common.Width || Y >= Common.Height)
                {
                    return false;
                }

                //We update our direction as we always end hitting wall at this point
                Direction = (Direction + 1) % 4;
            }
        }


        /// <summary>
        /// Finds the first point left of X,Y that is safe before hitting the wall
        /// </summary>
        /// <param name="X">Current Guard X</param>
        /// <param name="Y">Current Guard Y</param>
        /// <param name="blockX">Extra Wall X</param>
        /// <param name="blockY">Extra Wall Y</param>
        /// <returns></returns>
        public static (int X, int Y) GetLastSafeLeft(int X, int Y, int blockX, int blockY)
        {
            for (var x = X - 1; x >= 0; x--)
            {
                if ((x == blockX && Y == blockY) || (Common.Map[Y][x] == '#'))
                {
                    return (x + 1, Y);
                }
            }
            return (-1, Y);
        }

        /// <summary>
        /// Finds the first point right of X,Y that is safe before hitting the wall
        /// </summary>
        /// <param name="X">Current Guard X</param>
        /// <param name="Y">Current Guard Y</param>
        /// <param name="blockX">Extra Wall X</param>
        /// <param name="blockY">Extra Wall Y</param>
        /// <returns></returns>
        public static (int X, int Y) GetLastSafeRight(int X, int Y, int blockX, int blockY)
        {
            for (var x = X + 1; x < Common.Width; x++)
            {
                if ((x == blockX && Y == blockY) || (Common.Map[Y][x] == '#'))
                {
                    return(x - 1, Y);
                }
            }
            return (-1, Y);
        }

        /// <summary>
        /// Finds the first point up of X,Y that is safe before hitting the wall
        /// </summary>
        /// <param name="X">Current Guard X</param>
        /// <param name="Y">Current Guard Y</param>
        /// <param name="blockX">Extra Wall X</param>
        /// <param name="blockY">Extra Wall Y</param>
        /// <returns></returns>
        public static (int X, int Y) GetLastSafeUp(int X, int Y, int blockX, int blockY)
        {
            for (var y = Y - 1; y >= 0; y--)
            {
                if ((X == blockX && y == blockY) || (Common.Map[y][X] == '#'))
                {
                    return (X, y + 1);
                }
            }
            return (X, -1);
        }

        /// <summary>
        /// Finds the first point down of X,Y that is safe before hitting the wall
        /// </summary>
        /// <param name="X">Current Guard X</param>
        /// <param name="Y">Current Guard Y</param>
        /// <param name="blockX">Extra Wall X</param>
        /// <param name="blockY">Extra Wall Y</param>
        /// <returns></returns>
        public static (int X, int Y) GetLastSafeDown(int X, int Y, int blockX, int blockY)
        {
            for (var y = Y + 1; y < Common.Height; y++)
            {
                if ((X == blockX && y == blockY) || (Common.Map[y][X] == '#'))
                {
                    return (X, y - 1);
                }
            }
            return (X, -1);
        }
    }
}