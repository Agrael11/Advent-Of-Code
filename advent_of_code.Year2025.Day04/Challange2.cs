using advent_of_code.Helpers;

namespace advent_of_code.Year2025.Day04
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Parses the map
            var papers = Common.ParseInput(input);

            //Removes all that are accessible until none are, and returns number of removed items.
            return papers.RemoveAllWhere(papers.IsAccessible);
        }
    }
}