namespace advent_of_code.Year2019.Day12
{
    internal class Common
    {
        private static long GetVectorEnergy(long X, long Y, long Z)
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }

        public static long GetMoonEnergy(Moon moon)
        {
            return GetVectorEnergy(moon.Position.X, moon.Position.Y, moon.Position.Z) * GetVectorEnergy(moon.Velocity.X, moon.Velocity.Y, moon.Velocity.Z);
        }

        public static void Step(List<Moon> moons)
        {
            for (var i = 0; i < moons.Count - 1; i++)
            {
                for (var j = i + 1; j < moons.Count; j++)
                {
                    Influence(moons[i], moons[j]);
                }
            }
            foreach (var moon in moons)
            {
                moon.Position.X += moon.Velocity.X;
                moon.Position.Y += moon.Velocity.Y;
                moon.Position.Z += moon.Velocity.Z;
            }
        }

        private static void Influence(Moon moon1, Moon moon2)
        {
            var xInfluence = CalculateInfluence(moon1.Position.X, moon2.Position.X);
            var yInfluence = CalculateInfluence(moon1.Position.Y, moon2.Position.Y);
            var zInfluence = CalculateInfluence(moon1.Position.Z, moon2.Position.Z);
            moon1.Velocity.X += xInfluence;
            moon1.Velocity.Y += yInfluence;
            moon1.Velocity.Z += zInfluence;
            moon2.Velocity.X -= xInfluence;
            moon2.Velocity.Y -= yInfluence;
            moon2.Velocity.Z -= zInfluence;
        }

        private static long CalculateInfluence(long axis1, long axis2)
        {
            var result = axis2 - axis1;
            return (result == 0) ? 0 : (result > 0) ? 1 : -1;
        }
    }
}
