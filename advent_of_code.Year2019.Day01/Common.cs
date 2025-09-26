namespace advent_of_code.Year2019.Day01
{
    internal class Common
    {
        public static int CalculateFuel(int mass, bool recursive)
        {
            var fuel = mass / 3 - 2;
            return (fuel < 0) ? 0 : ((recursive) ? (fuel + CalculateFuel(fuel, true)) : fuel);
        }
    }
}
