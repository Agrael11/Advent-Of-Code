using System.Diagnostics.CodeAnalysis;

namespace advent_of_code.Year2015.Day06
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            bool[,] lightBulbsArray = new bool[1000, 1000];
            int lightBulbsOn = 0;

            foreach (string line in input)
            {
                string[] splitLine = line.Split(' ');

                if (splitLine[0] == "toggle")
                {
                    (int x, int y) corner1 = (int.Parse(splitLine[1].Split(',')[0]), int.Parse(splitLine[1].Split(',')[1]));
                    (int x, int y) corner2 = (int.Parse(splitLine[3].Split(',')[0]), int.Parse(splitLine[3].Split(',')[1]));
                    for (int x = corner1.x; x <= corner2.x; x++)
                    {
                        for (int y = corner1.y; y <= corner2.y; y++)
                        {
                            if (lightBulbsArray[x, y] == true) lightBulbsOn--;
                            else lightBulbsOn++;
                            lightBulbsArray[x, y] = !lightBulbsArray[x, y];
                        }
                    }
                }
                else if (splitLine[0] == "turn")
                {
                    (int x, int y) corner1 = (int.Parse(splitLine[2].Split(',')[0]), int.Parse(splitLine[2].Split(',')[1]));
                    (int x, int y) corner2 = (int.Parse(splitLine[4].Split(',')[0]), int.Parse(splitLine[4].Split(',')[1]));
                    if (splitLine[1] == "on")
                    {
                        for (int x = corner1.x; x <= corner2.x; x++)
                        {
                            for (int y = corner1.y; y <= corner2.y; y++)
                            {
                                if (lightBulbsArray[x, y] == false) lightBulbsOn++;
                                lightBulbsArray[x, y] = true;
                            }
                        }
                    }
                    else if (splitLine[1] == "off")
                    {
                        for (int x = corner1.x; x <= corner2.x; x++)
                        {
                            for (int y = corner1.y; y <= corner2.y; y++)
                            {
                                if (lightBulbsArray[x, y] == true) lightBulbsOn--;
                                lightBulbsArray[x, y] = false;
                            }
                        }
                    }
                }
            }

            return lightBulbsOn;
        }
    }
}
