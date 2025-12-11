namespace advent_of_code.Year2025.Day11
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //Parses input into Node Graph
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var nodes = Common.ParseInput(input);

            //And searches through nodes, from start to end
            return Common.DFSAll(nodes["you"], nodes["out"]);
        }
    }
}