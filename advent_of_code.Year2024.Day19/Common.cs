using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day19
{
    internal class Common
    {
        private static readonly Dictionary<char, Trie<char>> Patterns = new Dictionary<char, Trie<char>>();
        public static List<string> Parse(string[] input)
        {
            Patterns.Clear();
            foreach (var pattern in input[0].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (!Patterns.TryGetValue(pattern[0], out var trie))
                {
                    trie = new Trie<char>(pattern[0]);
                    Patterns.Add(pattern[0], trie);
                }
                AppendTrie(pattern[1..], trie);
            }
            return input[1..].ToList();
        }

        //Appends Trie
        private static void AppendTrie(string str, Trie<char> trie)
        {
            var workingTrie = trie;
            for (var i = 0; i < str.Length; i++)
            {
                workingTrie = workingTrie.GetOrAddBranch(str[i]);
            }
            workingTrie.GetOrAddBranch('$');
        }

        public static long CountPossiblePatterns(string targetDesign)
        {
            //Well going other way of DP is new to me
            //But simply - we count how many times pattern appeared at position "i"
            var dp = new long[targetDesign.Length + 1];
            //dp[0] is 1, because "" definitely fits
            dp[0] = 1;

            //Now we just go character by character of target
            for (var start = 0; start < targetDesign.Length; start++)
            {
                //If no pattern got us to this point, we cannot possibly get to end from here.
                var dpCount = dp[start];
                if (dpCount == 0) continue;

                //We'll get all patterns matching current character in trie.
                //Of course if any exist
                if (!Patterns.TryGetValue(targetDesign[start], out var current)) continue;

                var i = 0;
                while (current is not null) //Until we have any tries left to check
                {
                    i++; //We increment i - length of pattern
                    var end = start + i; //This is where pattern would put us

                    if (end > targetDesign.Length) break; // If we are out of bounds of dp, we end

                    if (current.ContainsBranch('$')) //And we count pattern as possible if it ends here ($ - terminator)
                    {
                        dp[end] += dpCount; //dpCount - how many times we got to beginning of pattern
                    }

                    if (end == targetDesign.Length) break; //If we're at end of string, there is no next character

                    current.TryGetBranch(targetDesign[end], out current); //We'll (try) to get next branch of trie
                } 
            }
            return dp[targetDesign.Length];
        }
    }
}
