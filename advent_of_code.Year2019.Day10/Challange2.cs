using advent_of_code.Helpers;
using System.Security.Principal;

namespace advent_of_code.Year2019.Day10
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Do same thing as part 1 and remove station from list
            var asteroids = Common.ParseInput(input);
            var (station, _) = Common.FindBestStation(asteroids);

            asteroids.Remove(station);

            //We create datastructe with all asteroids, assiging them their distance and normalized vector
            //Then we group them by normalized vector, order each group by distance,
            //and finally order the groups by angle of the normalized vector
            var asteroidsWithData = asteroids.Select(t => 
            {
                var vector = (t - station);
                var distance = vector.GetMagnitude();
                vector.NormalizeGCD();
                return (asteroid: t, distance, normal: vector); 
            }).GroupBy(t=>t.normal).Select(t=>t.OrderBy(t => t.distance).ToList())
            .OrderBy(t=>t.First().normal.GetAngleCustom()).ToList();

            var finalAsteroid = ShootUntil(asteroidsWithData, 200);

            return finalAsteroid.X * 100 + finalAsteroid.Y;
        }

        public static Vector2l ShootUntil(List<List<(Vector2l asteroid, double distance, Vector2l normal)>> asteroids,
            int target)
        {
            var theoreticalAngle = 0;
            var asteroid = new Vector2l(-1, -1);
            for (var shot = 1; shot <= target; shot++)
            {
                asteroid = Shoot(asteroids, ref theoreticalAngle);
            }
            return asteroid;
        }

        private static Vector2l Shoot(List<List<(Vector2l asteroid, double distance, Vector2l normal)>> asteroids, ref int index)
        {
            if (asteroids.Count == 0)
            {
                throw new InvalidOperationException("No asteroids left to shoot.");
            }

            var asteroid = asteroids[index][0].asteroid;

            asteroids[index].RemoveAt(0);
            index++;

            if (asteroids[index].Count == 0)
            {
                asteroids.RemoveAt(index);
                index--;
            }

            if (asteroids.Count > 0)
            {
                index %= asteroids.Count;
            }
            else
            {
                index = 0;
            }

            return asteroid;
        }
    }
}