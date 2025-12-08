namespace advent_of_code.Year2025.Day08
{
    public static class Challange1
    {
        private static readonly int ConnectionsToCheck = 1000;
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t=>new JunctionBox(t)).ToList();

            //We calculate distances and create empty list of cirucits
            var distances = Common.CalculateConnections(input);

            var circuits = new List<Circuit>();

            //For shortest *ConnectionsToCheck* (should be 1000) connections
            for (var i = 0; i < ConnectionsToCheck; i++)
            {
                var connection = distances[i];
                
                //We get their appropriate existing circuits (if exist)
                (var index1, var index2) = Common.GetCircuitsContaining(circuits, connection);

                //And add them into them (or create, or merge if needed)
                Common.AddToCircuits(circuits, index1, index2, connection);
            }

            //We order the circuits by the highest count of junction boxes within
            circuits = circuits.OrderByDescending(t => t.Count).ToList();

            //And multiple first 3
            var result = 1;
            for (var i = 0; i <3; i++)
            {
                result *= circuits[i].Count;
            }

            return result;
        }
    }
}