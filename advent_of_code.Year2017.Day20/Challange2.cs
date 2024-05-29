namespace advent_of_code.Year2017.Day20
{
    public static class Challange2
    {
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

            for (var i = particles.Count - 1; i >= 0; i--)
            {
                var collides = false;
                for (var j = i-1; j >= 0; j--)
                {
                    if (particles[i].Collides(particles[j]))
                    {
                        collides = true;
                        particles.RemoveAt(j);
                        i--;
                    }
                    else
                    {
                        var t = particles[i].CollideTime(particles[j]);
                        if (t > 0)
                        {
                            var ok = true;
                            for (var k = j-1; k >= 0; k--)
                            {
                                var t2 = particles[j].CollideTime(particles[k]);
                                if (t2 > 0 && t2 < t)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                            if (ok)
                            {
                                collides = true;
                                particles.RemoveAt(j);
                                i--;
                            }
                        }
                    }
                }
                if (collides)
                {
                    particles.RemoveAt(i);
                }
            }

            return particles.Count;
        }
    }
}