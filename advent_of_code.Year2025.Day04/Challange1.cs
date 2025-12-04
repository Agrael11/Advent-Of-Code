namespace advent_of_code.Year2025.Day04
{
    public static class Challange1
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Parses the paper map
            var papers = Common.ParseInput(input);

            //And counts number of accessible papers
            return papers.Count(papers.IsAccessible);
        }
    }
}