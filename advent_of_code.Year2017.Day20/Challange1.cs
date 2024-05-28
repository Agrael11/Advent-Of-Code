using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks.Sources;
using System.Xml;

namespace advent_of_code.Year2017.Day20
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var closest = -1;
            var closestAcc = long.MaxValue;
            var closestVel = long.MaxValue;
            var closestDist = long.MaxValue;
            for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
            {
                var line = input[lineIndex];
                var particle = new Particle(line, lineIndex);
                var acc = particle.GetManhattanAcceleration();
                var vel = particle.GetManhattanVelocity();
                var dist = particle.GetManhattanDistance();

                if ((acc < closestAcc) ||
                    (acc == closestAcc && vel < closestVel) ||
                    (acc == closestAcc && vel  == closestVel && dist < closestDist))
                {
                    closestAcc = acc;
                    closestVel = vel;
                    closestDist = dist;
                    closest = lineIndex;
                }
            }

            return closest;
        }
    }
}