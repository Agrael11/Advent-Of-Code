namespace advent_of_code.Year2019.Day20
{
    public static class Challange1
    {
        private static readonly (int offsetX, int offesetY)[] Directions = [
            (0, -1),
            (1, 0),
            (0, 1),
            (-1, 0) ];


        public static string DoChallange(string inputData)
        {
            //inputData = "         A           \r\n         A           \r\n  #######.#########  \r\n  #######.........#  \r\n  #######.#######.#  \r\n  #######.#######.#  \r\n  #######.#######.#  \r\n  #####  B    ###.#  \r\nBC...##  C    ###.#  \r\n  ##.##       ###.#  \r\n  ##...DE  F  ###.#  \r\n  #####    G  ###.#  \r\n  #########.#####.#  \r\nDE..#######...###.#  \r\n  #.#########.###.#  \r\nFG..#########.....#  \r\n  ###########.#####  \r\n             Z       \r\n             Z       ";
            //inputData = "                   A               \r\n                   A               \r\n  #################.#############  \r\n  #.#...#...................#.#.#  \r\n  #.#.#.###.###.###.#########.#.#  \r\n  #.#.#.......#...#.....#.#.#...#  \r\n  #.#########.###.#####.#.#.###.#  \r\n  #.............#.#.....#.......#  \r\n  ###.###########.###.#####.#.#.#  \r\n  #.....#        A   C    #.#.#.#  \r\n  #######        S   P    #####.#  \r\n  #.#...#                 #......VT\r\n  #.#.#.#                 #.#####  \r\n  #...#.#               YN....#.#  \r\n  #.###.#                 #####.#  \r\nDI....#.#                 #.....#  \r\n  #####.#                 #.###.#  \r\nZZ......#               QG....#..AS\r\n  ###.###                 #######  \r\nJO..#.#.#                 #.....#  \r\n  #.#.#.#                 ###.#.#  \r\n  #...#..DI             BU....#..LF\r\n  #####.#                 #.#####  \r\nYN......#               VT..#....QG\r\n  #.###.#                 #.###.#  \r\n  #.#...#                 #.....#  \r\n  ###.###    J L     J    #.#.###  \r\n  #.....#    O F     P    #.#...#  \r\n  #.###.#####.#.#####.#####.###.#  \r\n  #...#.#.#...#.....#.....#.#...#  \r\n  #.#####.###.###.#.#.#########.#  \r\n  #...#.#.....#...#.#.#.#.....#.#  \r\n  #.###.#####.###.###.#.#.#######  \r\n  #.#.........#...#.............#  \r\n  #########.###.###.#############  \r\n           B   J   C               \r\n           U   P   P               ";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var map = new Map(input);

            var result = BFSAroundTheMap(map);

            return result?.ToString()??"No path found";
        }

        private static int? BFSAroundTheMap(Map map)
        {

            var startPoint = map.Portals.Where(t=>t.Value.Name == "AA").Select(t=>t.Value).First();
            var endPoint = map.Portals.Where(t=>t.Value.Name == "ZZ").Select(t=>t.Value).First();
            var queue = new Queue<(int x, int y, int steps)>();
            var visited = new HashSet<(int x, int y)>();

            queue.Enqueue((startPoint.Target.X, startPoint.Target.Y, 0));
            while (queue.Count> 0)
            {
                var (currentX, currentY, currentSteps) = queue.Dequeue();
                if (!visited.Add((currentX, currentY)))
                {
                    continue;
                }
                if (currentX == endPoint.Target.X && currentY == endPoint.Target.Y)
                {
                    return currentSteps;
                }

                foreach (var (offsetX, offesetY) in Directions)
                {
                    var nextX = currentX + offsetX;
                    var nextY = currentY + offesetY;
                    if (!map.Paths.Contains((nextX, nextY)))
                    {
                        continue;
                    }
                    if (map.Portals.TryGetValue((nextX, nextY), out var portal) && portal.Other is not null)
                    {
                        var (portalTargetX, portalTargetY) = portal.Other.Target;
                        queue.Enqueue((portalTargetX, portalTargetY, currentSteps + 1));
                    }
                    else
                    {
                        queue.Enqueue((nextX, nextY, currentSteps + 1));
                    }
                }
            }

            return null;
        }
    }
}