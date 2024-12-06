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


            //To allow multicore processing, we create multiple jobs that run concurrently, but since the code is slower, we will run it in larger batches
            var batches = new List<BatchJob<bool, int>>();

            //That's how we cound nuumber of items in batch :)
            var batch = new BatchJob<bool, int>((data) => data.Count(v => v == true));

            foreach (var (locationX, locationY) in visitedLocations)
            {
                //Each job checks if path guard takes would be a loop
                batch.Jobs.Add(new SingleJob<bool>(() => WouldBeLoop(guardX, guardY, guardDirection, locationX, locationY)));

                //If batch is full, we add it to list and create next one
                if (batch.Size == JOBCOUNT)
                {
                    batches.Add(batch);
                    batch = new BatchJob<bool, int>((data) => data.Count(v => v == true));
                }
            }

            //If there's incomplete batch, we also add it
            if (batch.Size > 0) batches.Add(batch);

            //We execute batches in parallel
            Parallel.ForEach(batches, batch => batch.Run());

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