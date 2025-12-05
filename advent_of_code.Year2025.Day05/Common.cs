namespace advent_of_code.Year2025.Day05
{
    internal static class Common
    {
        /// <summary>
        /// Parses the input into list of ranges, and list of ids
        /// </summary>
        /// <param name="inputLines"></param>
        /// <returns></returns>
        public static (List<InclusiveRange> ranges, List<long> ids) Parse(string[] inputLines)
        {
            //Preparation of lists
            List<InclusiveRange> ranges = [];
            List<long> ids = [];
            
            //We start parsing ranges until we hit empty line (a divider between ranges and ids)
            var lineIndex = 0;
            for (; !string.IsNullOrWhiteSpace(inputLines[lineIndex]); lineIndex++)
            {
                var rangeInfo = inputLines[lineIndex].Split('-');
                ranges.Add(new (long.Parse(rangeInfo[0]), long.Parse(rangeInfo[1])));
            }
            
            //This skips said empty line, and then continutes parsing ids
            lineIndex++;
            
            for (; lineIndex < inputLines.Length; lineIndex++)
            {
                ids.Add(long.Parse(inputLines[lineIndex]));
            }
            
            return (ranges, ids);
        }
    }
}
