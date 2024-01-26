namespace advent_of_code.Year2015.Day06
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var ligthBulbsArray = new int[1000, 1000];
            var lightBulbsOn = 0;

            foreach (var line in input)
            {
                var splitLine = line.Split(' ');

                if (splitLine[0] == "toggle")
                {
                    (int x, int y) corner1 = (int.Parse(splitLine[1].Split(',')[0]), int.Parse(splitLine[1].Split(',')[1]));
                    (int x, int y) corner2 = (int.Parse(splitLine[3].Split(',')[0]), int.Parse(splitLine[3].Split(',')[1]));
                    for (var x = corner1.x; x <= corner2.x; x++)
                    {
                        for (var y = corner1.y; y <= corner2.y; y++)
                        {
                            lightBulbsOn += 2;
                            ligthBulbsArray[x, y] += 2;
                        }
                    }
                }
                else if (splitLine[0] == "turn")
                {
                    (int x, int y) corner1 = (int.Parse(splitLine[2].Split(',')[0]), int.Parse(splitLine[2].Split(',')[1]));
                    (int x, int y) corner2 = (int.Parse(splitLine[4].Split(',')[0]), int.Parse(splitLine[4].Split(',')[1]));
                    if (splitLine[1] == "on")
                    {
                        for (var x = corner1.x; x <= corner2.x; x++)
                        {
                            for (var y = corner1.y; y <= corner2.y; y++)
                            {
                                lightBulbsOn++;
                                ligthBulbsArray[x, y]++;
                            }
                        }
                    }
                    else if (splitLine[1] == "off")
                    {
                        for (var x = corner1.x; x <= corner2.x; x++)
                        {
                            for (var y = corner1.y; y <= corner2.y; y++)
                            {
                                if (ligthBulbsArray[x, y] > 0)
                                {
                                    lightBulbsOn--;
                                    ligthBulbsArray[x, y]--;
                                }
                            }
                        }
                    }
                }
            }

            return lightBulbsOn;
        }
    }
}
