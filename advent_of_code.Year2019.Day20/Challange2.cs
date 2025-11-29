namespace advent_of_code.Year2019.Day20
{
    public static class Challange2
    {
        private static readonly (int offsetX, int offesetY)[] Directions = [
            (0, -1),
            (1, 0),
            (0, 1),
            (-1, 0) ];


        public static string DoChallange(string inputData)
        {
            //inputData = "         A           \r\n         A           \r\n  #######.#########  \r\n  #######.........#  \r\n  #######.#######.#  \r\n  #######.#######.#  \r\n  #######.#######.#  \r\n  #####  B    ###.#  \r\nBC...##  C    ###.#  \r\n  ##.##       ###.#  \r\n  ##...DE  F  ###.#  \r\n  #####    G  ###.#  \r\n  #########.#####.#  \r\nDE..#######...###.#  \r\n  #.#########.###.#  \r\nFG..#########.....#  \r\n  ###########.#####  \r\n             Z       \r\n             Z       ";
            //inputData = "             Z L X W       C                 \r\n             Z P Q B       K                 \r\n  ###########.#.#.#.#######.###############  \r\n  #...#.......#.#.......#.#.......#.#.#...#  \r\n  ###.#.#.#.#.#.#.#.###.#.#.#######.#.#.###  \r\n  #.#...#.#.#...#.#.#...#...#...#.#.......#  \r\n  #.###.#######.###.###.#.###.###.#.#######  \r\n  #...#.......#.#...#...#.............#...#  \r\n  #.#########.#######.#.#######.#######.###  \r\n  #...#.#    F       R I       Z    #.#.#.#  \r\n  #.###.#    D       E C       H    #.#.#.#  \r\n  #.#...#                           #...#.#  \r\n  #.###.#                           #.###.#  \r\n  #.#....OA                       WB..#.#..ZH\r\n  #.###.#                           #.#.#.#  \r\nCJ......#                           #.....#  \r\n  #######                           #######  \r\n  #.#....CK                         #......IC\r\n  #.###.#                           #.###.#  \r\n  #.....#                           #...#.#  \r\n  ###.###                           #.#.#.#  \r\nXF....#.#                         RF..#.#.#  \r\n  #####.#                           #######  \r\n  #......CJ                       NM..#...#  \r\n  ###.#.#                           #.###.#  \r\nRE....#.#                           #......RF\r\n  ###.###        X   X       L      #.#.#.#  \r\n  #.....#        F   Q       P      #.#.#.#  \r\n  ###.###########.###.#######.#########.###  \r\n  #.....#...#.....#.......#...#.....#.#...#  \r\n  #####.#.###.#######.#######.###.###.#.#.#  \r\n  #.......#.......#.#.#.#.#...#...#...#.#.#  \r\n  #####.###.#####.#.#.#.#.###.###.#.###.###  \r\n  #.......#.....#.#...#...............#...#  \r\n  #############.#.#.###.###################  \r\n               A O F   N                     \r\n               A A D   M                     ";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var map = new Map(input);

            var result = BFSAroundTheMap(map);

            return result?.ToString() ?? "No path found";
        }

        private static int? BFSAroundTheMap(Map map)
        {

            var startPoint = map.Portals.Where(t => t.Value.Name == "AA").Select(t => t.Value).First();
            var endPoint = map.Portals.Where(t => t.Value.Name == "ZZ").Select(t => t.Value).First();
            var queue = new Queue<(int x, int y, int level, int steps)>();
            var visited = new HashSet<(int x, int y, int level)>();

            queue.Enqueue((startPoint.Target.X, startPoint.Target.Y, 0, 0));
            while (queue.Count > 0)
            {
                var (currentX, currentY, currentLevel, currentSteps) = queue.Dequeue();
                if (!visited.Add((currentX, currentY, currentLevel)))
                {
                    continue;
                }
                if (currentX == endPoint.Target.X && currentY == endPoint.Target.Y && currentLevel == 0)
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
                    if (map.Portals.TryGetValue((nextX, nextY), out var portal))
                    {
                        if (portal.Other is not null)
                        {
                            var nextLevel = currentLevel + ((portal.Outer) ? -1 : 1);
                            if (nextLevel < 0)
                            {
                                continue;
                            }
                            var (portalTargetX, portalTargetY) = portal.Other.Target;
                            queue.Enqueue((portalTargetX, portalTargetY, nextLevel, currentSteps + 1));
                        }
                        else if (currentLevel == 0)
                        {
                            queue.Enqueue((nextX, nextY, currentLevel, currentSteps + 1));
                        }
                    }
                    else
                    {
                        queue.Enqueue((nextX, nextY, currentLevel, currentSteps + 1));
                    }
                }
            }

            return null;
        }
    }
}