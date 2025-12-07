namespace advent_of_code.Year2025.Day07
{
    internal static class Common
    {
        public static readonly int[] Offsets = [-1, +1];

        /// <summary>
        /// Parses the input into dictionary of manifolds, with entry point as one (even if it realistically isn't, but hey - easier math.)
        /// </summary>
        /// <param name="input">Input map</param>
        /// <returns>Tuple of manifold dictioanry and entry point manifold</returns>
        /// <exception cref="Exception">May throw if unexpected character appears in map or no valid start point is found</exception>
        public static (Dictionary<(int X, int Y), Manifold> manifolds, Manifold entryPoint) ParseMap(string[] input)
        {
            var entryPoint = new Manifold(0, 0);
            var manifolds = new Dictionary<(int x, int y), Manifold>();

            //We standardly go through map as grid
            for (var y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var character = line[x];
                    switch (character)
                    {
                        case 'S':
                            entryPoint = new Manifold(x, y); //S is start point
                            break;
                        case '^':
                            manifolds[(x, y)] = new Manifold(x, y); //^ is mmanifold
                            break;
                        case '.': //. is really an empty spot
                            break;
                        default:
                            throw new Exception("Unexpected character");
                    }
                }
            }

            var sortedManifolds = manifolds.OrderBy(t => t.Key.x).ThenBy(t => t.Key.y).ToDictionary();

            //We find first lower manifold to start point and set it's connection so startpoint can be used to calculate info
            if (!sortedManifolds.TryGetFirstEncountered((entryPoint.Position.X, entryPoint.Position.Y), out var encountered))
            {
                throw new Exception("No start point found");
            }
            entryPoint.AddConnection(sortedManifolds[encountered]);

            return (sortedManifolds, entryPoint);
        }

        /// <summary>
        /// Finds connectionns that manifold splitters have between each other when splitting tachyon rays.
        /// </summary>
        /// <param name="manifoldMap"></param>
        public static void GenerateConnections(Dictionary<(int, int), Manifold> manifoldMap)
        {
            foreach (var manifold in manifoldMap.Values)
            {
                //Tachyon is split to left and right of manifold.
                foreach (var offset in Offsets)
                {
                    if (manifoldMap.TryGetFirstEncountered((manifold.Position.X + offset, manifold.Position.Y), out var encountered)) //If it hits manifold, I consider it connected node.
                    {
                        manifold.AddConnection(manifoldMap[encountered]);
                    }
                }
            }
        }

        /// <summary>
        /// Tries to find nearest lower manifold to requested position
        /// </summary>
        /// <param name="map"></param>
        /// <param name="position"></param>
        /// <param name="foundManifold"></param>
        /// <returns></returns>
        public static bool TryGetFirstEncountered(this Dictionary<(int X, int Y), Manifold> map, (int X, int Y) position, out (int X, int Y) foundManifold)
        {
            foundManifold = (-1, -1);

            //We filter manifolds so only ones in same X location (we are looking down) and only under us.
            var filtered = map.Where(item => item.Key.X == position.X && item.Key.Y > position.Y);

            //If none are found we return false
            if (!filtered.Any())
            {
                return false;
            }

            //otherwise we take first one - it is already sorted, so it will be correct.
            foundManifold = filtered.First().Key;
            return true;
        }
    }
}
