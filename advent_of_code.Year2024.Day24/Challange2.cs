namespace advent_of_code.Year2024.Day24
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Parse(input);

            //We find the corrupt half of pairs - the ones that contain XOR when they should not
            //and connects them to their corresponding "other"
            var wrong = FindEasyCorruptPairParts();
            var corruptPairs = PairToNonZs(wrong);
            
            //We swap the pairs and find solution for configuration
            Common.SwapPairs(corruptPairs);
            Common.Solve();

            //After that we figure out where the expected result (x+y) differs from our result (z)
            var x = Common.GetResult('x');
            var y = Common.GetResult('y');
            var z = Common.GetResult('z');
            var expectedZ = x + y;
            var point = FindPointOfDifference(z, expectedZ);
            var searchX = "x" + point;
            var searchY = "y" + point;

            //Now we re-parse the data, as they were modified when "Solve" was called
            Common.Parse(input);

            //We two remaining pairs that don't work - they are at the point where the difference happenned
            //Those are corrupted and are added to list of pairs
            var data = Common.Connections.Where(calculation =>
            ((calculation.Value.first == searchX || calculation.Value.first == searchY) &&
            (calculation.Value.second == searchX || calculation.Value.second == searchY))
                ).Select(calculation => calculation.Key).ToList();

            corruptPairs.Add((data[0], data[1]));

            //Here we just create one single list of all parts of all pairs ordered alphabetically
            //And return it, divided by commas
            var connections = corruptPairs.SelectMany(pair => new[] {pair.Item1, pair.Item2 }).Order();

            return string.Join(',', connections);
        }

        private static List<string> FindEasyCorruptPairParts()
        {
            var wrong = new List<string>();

            foreach ((var target, var operation) in Common.Connections)
            {
                if (target[0] != 'z')
                {
                    //If connection does not result in "z" and doesn't have x and y at same time
                    var hasX = operation.first[0] == 'x' || operation.second[0] == 'x';
                    var hasY = operation.first[0] == 'y' || operation.second[0] == 'y';
                    
                    //It is corrupt if it is XOR - XORs are exclusive for creation of z and x-y operations
                    if (!(hasX && hasY) && operation.operation == 2) wrong.Add(target);
                }
            }

            return wrong;
        }

        /// <summary>
        /// Pairs up each corrupt non "z-targetting" connection to it's correspoding "z-targetting" connection
        /// </summary>
        /// <param name="wrongNonZs"></param>
        /// <returns></returns>
        private static List<(string, string)> PairToNonZs(List<string> wrongNonZs)
        {
            var pairs = new List<(string, string)>();
            foreach (var wrongnonZ in wrongNonZs)
            {
                var replacer = FindCorruptZ(wrongnonZ);
                pairs.Add((wrongnonZ, replacer));
            }
            return pairs;
        }
        
        /// <summary>
        /// Finds the closest lower Z - this is corresponding corrupt Z to this wrong point
        /// Uses BFS to find closest HIGHER Z and subtrictes one from it
        /// </summary>
        /// <param name="wrong"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string FindCorruptZ(string wrong)
        {
            var queue = new Queue<string>();
            queue.Enqueue(wrong);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current[0] == 'z')
                {
                    return "z" + (int.Parse(current[1..]) - 1);
                }
                foreach (var next in Common.Connections.Where(connection => connection.Value.first == current || connection.Value.second == current))
                {
                    queue.Enqueue(next.Key);
                }
            }

            throw new Exception("No Z conneciton is found for " + wrong);
        }

        /// <summary>
        /// Finds the index of first x and y that produce incorrect z
        /// that is found by xoring expected and current value, and finding first bit that is "1" - therefore different.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        private static int FindPointOfDifference(long current, long expected)
        {
            var bitPosition = 0;
            var difference = current ^ expected;
            while ((difference & 1) == 0)
            {
                difference >>= 1;
                bitPosition++;
            }
            return bitPosition;
        }
    }
}