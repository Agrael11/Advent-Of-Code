namespace advent_of_code.Year2025.Day08
{
    internal static class Common
    {
        /// <summary>
        /// Pre-fills list of connections between each two junctionboxes
        /// Only one-directional. Distance is calcualted by connection itself
        /// List is ordered by distance
        /// </summary>
        /// <param name="junctionBoxes">List of junction boxes</param>
        /// <returns>List of ordered connections</returns>
        public static List<Connection> CalculateConnections(List<JunctionBox> junctionBoxes)
        {
            var distances = new List<Connection>();

            for (var i = 0; i < junctionBoxes.Count - 1; i++)
            {
                var first = junctionBoxes[i];
                for (var j = i + 1; j < junctionBoxes.Count; j++)
                {
                    var second = junctionBoxes[j];
                    distances.Add(new Connection(first, second));
                }
            }

            return distances.OrderBy(t => t.Distance).ToList();
        }

        /// <summary>
        /// Finds a circuit containing a boxes. If none contain box, it returns -1 for the box
        /// </summary>
        /// <param name="circuits">List of circuits</param>
        /// <param name="connection">Conneciton to check</param>
        /// <returns>Tuple of first and second index</returns>
        public static (int firstIndex, int secondnIndex) GetCircuitsContaining(List<Circuit> circuits, Connection connection)
        {
            var index1 = -1;
            var index2 = -1;

            for (var index = 0; index < circuits.Count; index++)
            {
                var circuit = circuits[index];
                if (circuit.Contains(connection.FirstBox)) index1 = index;
                if (circuit.Contains(connection.SecondBox)) index2 = index;
                if (index1 != -1 && index2 != -1) break;
            }

            return (index1, index2);
        }

        /// <summary>
        /// Adds boxes to connencted circuits. Creates or Merges circuits if needed.
        /// </summary>
        /// <param name="circuits">List of Circuits</param>
        /// <param name="index1">Index of First Circuit (-1 for none)</param>
        /// <param name="index2">Index of Second Circuit (-1 for none)</param>
        /// <param name="connection">Connection to add</param>
        public static void AddToCircuits(List<Circuit> circuits, int index1, int index2, Connection connection)
        {
            //If neither cirucit exists, creates new one
            if (index1 == -1 && index2 == -1)
            {
                var newCircuit = new Circuit();
                newCircuit.Add(connection.FirstBox);
                newCircuit.Add(connection.SecondBox);
                circuits.Add(newCircuit);
                return;
            }
            
            //If second (or first) circuit exists, adds the first (or second) box into it
            if (index1 >= 0 && index2 == -1)
            {
                circuits[index1].Add(connection.SecondBox);
                return;
            }
            
            if (index2 >= 0 && index1 == -1)
            {
                circuits[index2].Add(connection.FirstBox);
                return;
            }
            
            //If each of boxes connects to different circuit, merges them
            if (index1 >= 0 && index2 >= 0 && index1 != index2)
            {
                circuits[index1].UnionWith(circuits[index2]);
                circuits.RemoveAt(index2);
                return;
            }
        }
    }
}
