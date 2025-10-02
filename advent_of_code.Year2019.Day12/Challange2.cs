namespace advent_of_code.Year2019.Day12
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var moons = input.Select(moonInfo => new Moon(moonInfo)).ToList();

            var loops = new TrippleState[4];

            for (var i = 0; i < 4; i++)
            {
                loops[i] = new TrippleState();
            }

            for (var steps = 0; steps <= 572663; steps++)
            {
                Common.Step(moons);

                for (var moon = 0; moon < 4; moon++)
                {
                    if (!loops[moon].LoopFound)
                    {
                        loops[moon].Add((moons[moon].Position, moons[moon].Velocity));
                    }    
                }

                if (loops.All(l => l.LoopFound))
                {
                    break;
                }
            }

            var LCMchain = 1L;
            foreach (var loop in loops)
            {
                LCMchain = Helpers.MathHelpers.LCM(LCMchain, loop.LoopLength);
            }

            return LCMchain;
            
        }
    }
}