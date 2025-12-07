namespace advent_of_code.Year2025.Day07
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            //Parses the input Map
            (var manifoldMap, _) = Common.ParseMap(input);

            //Navigetes the map and generates connection
            Common.GenerateConnections(manifoldMap);

            //Counts number of visited manifolds = how many times the beam will split
            return manifoldMap.Count(t=>t.Value.Visited);
        }
    }
}