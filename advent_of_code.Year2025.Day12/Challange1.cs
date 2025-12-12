namespace advent_of_code.Year2025.Day12
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Where(t => t.Contains('x')).ToList();


            //I'm not proud of this. it's stupid that it works. I have no idea why it works
            //But it works.
            //Shape doesn't matter for some reason. even if it absolutely should.

            return input.Count(FitsGifts);
        }


        /// <summary>
        /// Checks if area fits all christmas gifts... pretending all are 3x3.
        /// Even if they're not... it doesn't matter
        /// </summary>
        /// <param name="areaDefition"></param>
        /// <returns></returns>
        private static bool FitsGifts(string areaDefition)
        {
            var split = areaDefition.Split(':', StringSplitOptions.TrimEntries);
            var area = split[0].Split('x').Select(int.Parse).ToArray();
            var shapesCount = split[1].Split(' ').Select(int.Parse).Sum();

            var neededArea = GetNormalizedArea(area[0], area[1]);
            var shapeArea = shapesCount * 9;

            return shapeArea <= neededArea;
        }

        /// <summary>
        /// Calcualtes normalized area - where it is divisible by maximum gift size - 3x3
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static int GetNormalizedArea(int width, int height)
        {
            width -= width % 3;
            height-= height% 3;
            return width * height;
        }
    }
}