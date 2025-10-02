namespace advent_of_code.Year2019.Day12
{
    public static class Challange1
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            var moons = input.Select(moonInfo => new Moon(moonInfo)).ToList();

            var steps = 1000;

            for (var i = 0; i < steps; i++)
            {
                Common.Step(moons);
            }

            return moons.Sum(Common.GetMoonEnergy);
        }
    }
}