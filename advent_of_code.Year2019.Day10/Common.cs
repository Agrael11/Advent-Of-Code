using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day10
{
    internal class Common
    {
        public static List<Vector2l> ParseInput(string[] input)
        {
            var asteroids = new List<Vector2l>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        asteroids.Add(new Vector2l(x, y));
                    }
                }
            }

            return asteroids;
        }

        public static (Vector2l station, int visible) FindBestStation(List<Vector2l> asteroids)
        {
            return asteroids.Select(t => (asteroid: t, distance: GetVisibleAsteroidsCount(t, asteroids)))
                .MaxBy(t => t.distance);
        }

        private static int GetVisibleAsteroidsCount(Vector2l viewer, List<Vector2l> all)
        {
            return all.Select(t => { var o = (t - viewer); o.NormalizeGCD(); return o; })
                .Where(t => t.X != 0 || t.Y != 0).Distinct().Count();
        }
    }
}
