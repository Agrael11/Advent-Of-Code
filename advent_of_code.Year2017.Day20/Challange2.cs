namespace advent_of_code.Year2017.Day20
{
    public static class Challange2
    {
        private static readonly int ExtraSteps = 100; 

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var particles = new List<Particle>();
            for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
            {
                var line = input[lineIndex];
                var particle = new Particle(line, lineIndex);
                particles.Add(particle);
            }

            var extra = 0;
            while (extra < ExtraSteps)
            {
                extra++;
                foreach (var particle in particles)
                {
                    particle.SimulateStep();
                }

                for (var i = particles.Count-1; i >= 0; i--)
                {
                    var particle = particles[i];
                    var collided = false;
                    for (var j = i-1; j >= 0; j--)
                    {
                        if (particle.Collides(particles[j]))
                        {
                            collided = true;
                            particles.RemoveAt(j);
                            i--;
                        }
                    }
                    if (collided)
                    {
                        particles.RemoveAt(i);
                        extra = 0;      
                        continue;
                    }
                }
            }

            return particles.Count;
        }
    }
}