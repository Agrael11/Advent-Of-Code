namespace advent_of_code.Year2024.Day24
{
    internal static class Common
    {
        public static readonly Dictionary<string, bool> Devices = new Dictionary<string, bool>();
        public static readonly Dictionary<string, (string first, string second, int operation)> Connections =
            new Dictionary<string, (string, string, int)>();


        public static void Parse(string[] input)
        {
            Devices.Clear();
            Connections.Clear();

            var parsingConnections = false;
            foreach (var line in input)
            {
                if (parsingConnections)
                {
                    //Parsing connection in format source1 OPEPRAND source2 -> target
                    var splitConnection = line.Split("->", StringSplitOptions.TrimEntries);
                    var operation = splitConnection[0].Split(' ');
                    var operand = operation[1] switch
                    {
                        "OR" => 0,
                        "AND" => 1,
                        "XOR" => 2,
                        _ => throw new Exception($"Unknown Operand: {operation[1]}")
                    };
                    Connections.Add(splitConnection[1], (operation[0], operation[2], operand));
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    parsingConnections = true;
                    continue;
                }
                //Parse default state in foramt target: state
                var splitDefinition = line.Split(':', StringSplitOptions.TrimEntries);
                Devices.Add(splitDefinition[0], (splitDefinition[1] == "1"));
            }
        }


        /// <summary>
        /// Calculates binary result based on first letter of devices. Devices are in format xyy where x is letter and yy is two digit number
        /// Numbers represent index of binary digit
        /// </summary>
        /// <param name="firstChar">first letter in device name</param>
        /// <returns>Binary Result</returns>
        public static long GetResult(char firstChar)
        {
            var zDevices = Devices.Where(device => (device.Key[0] == firstChar && device.Value)).Select(device => int.Parse(device.Key[1..]));
            
            return zDevices.Sum(zDevice => (long)Math.Pow(2, zDevice));
        }

        /// <summary>
        /// Swaps pairs of connections
        /// </summary>
        /// <param name="pairs"></param>
        public static void SwapPairs(List<(string first, string second)> pairs)
        {
            foreach (var (first, second) in pairs)
            {
                (Connections[first], Connections[second]) = (Connections[second], Connections[first]);
            }
        }

        public static void Solve()
        {
            //While we have unresolved devices
            while (Connections.Count > 0)
            {
                Solve(Connections.Keys.First(Resolveable)); //Gets first one that is possible to resolve and resolve it
            }
        }

        private static void Solve(string target)
        {
            //Get connection device and its operands
            var connection = Connections[target];
            var first = Devices[connection.first];
            var second = Devices[connection.second];

            //Does the operation
            var result = connection.operation switch
            {
                0 => first | second,
                1 => second & first,
                2 => first ^ second,
                _ => throw new Exception("Incorrect operation"),
            };

            //And adds device to list of Resolved Devices and removes it from list of unresolved ones
            Devices.Add(target, result);
            Connections.Remove(target);
        }

        /// <summary>
        /// Checks if Connection target is resolveable
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static bool Resolveable(string target)
        {
            var firstKey = Connections[target].first;
            var secondKey = Connections[target].second;
            return Devices.ContainsKey(firstKey) && Devices.ContainsKey(secondKey);
        }
    }
}
