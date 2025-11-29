namespace advent_of_code.Year2019.Day20
{

    internal class Map
    {
        public HashSet<(int x, int y)> Paths = [];
        public Dictionary<(int x, int y), Portal> Portals = [];

        public Map(string[] input)
        {
            Dictionary<string, (int x, int y)> tempPortals = [];
            
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == '.')
                    {
                        Paths.Add((x, y));
                    }
                    else if (input[y][x] != '#' && input[y][x] != ' ' && TryReadPortal(x, y, input, out var portalName, out var portalDirection, out var portalOuter))
                    {
                        var thisPortal = new Portal((x, y), portalDirection, portalOuter, portalName);
                        Portals[(x,y)] = thisPortal;
                        Paths.Add((x, y));

                        if (!tempPortals.TryGetValue(portalName, out var position))
                        {
                            tempPortals.Add(portalName, (x, y));
                        }
                        else
                        {
                            var otherPortal = Portals[position];
                            thisPortal.Other = otherPortal;
                            otherPortal.Other = thisPortal;
                        }
                    }
                }
            }

            Portals = Portals.OrderBy(Portals => Portals.Value.Name).ToDictionary(k => k.Key, v => v.Value);
        }

        private static bool TryReadPortal(int x, int y, string[] map, out string portalName, out (int X, int Y) portalDirection, out bool portalOuter)
        {
            portalName = "";
            portalDirection = (0, 0);
            portalOuter = false;
            if (x == 1 || y == 1 || x == map[0].Length - 2 || y == map.Length - 2)
            {
                portalOuter = true;
            }
            if (y < 1 || y >= map.Length - 1 ||
                x < 1 || x >= map[y].Length - 1)
            {
                return false;
            }

            if (map[y+1][x] == '.')
            {
                portalName = $"{map[y - 1][x]}{map[y][x]}";
                portalDirection = (0, 1);
                return true;
            }
            if (map[y - 1][x] == '.')
            {
                portalName = $"{map[y][x]}{map[y + 1][x]}";
                portalDirection = (0, -1);
                return true;
            }
            if (map[y][x + 1] == '.')
            {
                portalName = $"{map[y][x - 1]}{map[y][x]}";
                portalDirection = (1, 0);
                return true;
            }
            if (map[y][x - 1] == '.')
            {
                portalName = $"{map[y][x]}{map[y][x + 1]}";
                portalDirection = (-1, 0);
                return true;
            }

            return false;
        }
    }
}
