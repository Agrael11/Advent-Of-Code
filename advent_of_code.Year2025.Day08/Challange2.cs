namespace advent_of_code.Year2025.Day08
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t => new JunctionBox(t)).ToList();

            //Same as part 1
            var distances = Common.CalculateConnections(input);

            var circuits = new List<Circuit>();

            //Except we go for ALL distances found
            for (var i = 0; i < distances.Count; i++)
            {
                var connection = distances[i];

                (var index1, var index2) = Common.GetCircuitsContaining(circuits, connection);

                Common.AddToCircuits(circuits, index1, index2, connection);

                //That is until we are merged into single circuit with all junction boxes in
                if (circuits.Count == 1 && circuits[0].Count == input.Count)
                {
                    return connection.FirstBox.X * connection.SecondBox.X;
                }
            }
            
            //-1 = none found
            return -1;
        }
    }
}