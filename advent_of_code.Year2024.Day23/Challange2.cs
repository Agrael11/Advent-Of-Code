namespace advent_of_code.Year2024.Day23
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            Common.Parse(input);
            var largest = new HashSet<string>();

            foreach (var result in BrothKerbal1(new HashSet<string>(), Common.Nodes.Keys.ToHashSet(), new HashSet<string>()))
            {
                if (result.Count > largest.Count)
                {
                    largest = result;
                }
            }
            return string.Join(',', largest.Order());
        }

        /// <summary>
        /// Bron-Kerbosh algorithm
        /// https://en.wikipedia.org/wiki/Bron%E2%80%93Kerbosch_algorithm
        /// </summary>
        /// <param name="R">Working set (and current result if other sets are empty)</param>
        /// <param name="P">Potential set</param>
        /// <param name="X">Exclusion set... Because "eXcellent set" is not fancy enough</param>
        /// <returns></returns>
        private static IEnumerable<HashSet<string>> BrothKerbal1(HashSet<string> R, HashSet<string> P, HashSet<string> X)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                yield return R;
            }
            else
            {
                foreach (var node in P) 
                {
                    foreach (var result in BrothKerbal1(
                        R.Union([node]).ToHashSet(), 
                        P.Intersect(Common.Nodes[node].ConnectedIDS).ToHashSet(), 
                        X.Intersect(Common.Nodes[node].ConnectedIDS).ToHashSet()))
                    {
                        yield return result;
                    }
                    P.Remove(node);
                    X.Add(node);
                }
            }
        }
    }
}