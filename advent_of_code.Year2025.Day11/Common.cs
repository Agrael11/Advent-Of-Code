namespace advent_of_code.Year2025.Day11
{
    internal static class Common
    {
        /// <summary>
        /// Parses input
        /// </summary>
        /// <param name="input">Input of Node defintions - one per line</param>
        /// <returns>Dictionary of Nodes</returns>
        public static Dictionary<string, Node> ParseInput(string[] input)
        {
            var nodes = new Dictionary<string, Node>();

            foreach (var line in input)
            {
                //Each line is Node: ConnectedNodes
                var mainSplit = line.Split(':', StringSplitOptions.TrimEntries);
                var firstNode = nodes.GetOrAddNew(mainSplit[0]);
                
                //And each connected node is separated by space
                var connectedNodesNames = mainSplit[1].Split(' ');
                foreach (var connectedNode in connectedNodesNames.Select(nodes.GetOrAddNew))
                {
                    firstNode.ConnectedNodes.Add(connectedNode);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Helper that gets node from dictionary, and creates and adds it if needed
        /// </summary>
        /// <param name="dictionary">Affected dictionary</param>
        /// <param name="nodeName">Node to search for</param>
        /// <returns>Node in dictionary</returns>
        public static Node GetOrAddNew(this Dictionary<string, Node> dictionary, string nodeName)
        {
            if (!dictionary.TryGetValue(nodeName, out var node))
            {
                node = new Node(nodeName);
                dictionary.Add(nodeName, node);
            }
            return node;
        }

        /// <summary>
        /// Ugly memoized DFS using stack
        /// </summary>
        /// <param name="startNode">Starting node of DFS</param>
        /// <param name="targetNode">Target node of DFS</param>
        /// <returns>Count of possible paths</returns>
        public static long DFSAll(Node startNode, Node targetNode)
        {
            //Main stack with starting node in it
            var stack = new Stack<Node>();
            stack.Push(startNode);

            //Memoization stuff
            var parents = new Dictionary<Node, Node>();
            var memo = new Dictionary<Node, long>();

            //Simplify memo updating
            //note had no idea you can make local functions in this way - instead of name = (..)=>{};
            void DFSAll_UpdateMemo(Node startNode, long updateCount)
            {
                var node = startNode;
                while (parents.TryGetValue(node, out node))
                {
                    if (!memo.ContainsKey(node)) memo[node] = 0;
                    memo[node] += updateCount;
                }
            }

            //Result aggregator
            var paths = 0L;

            //And here we go again... until we explored all
            while (stack.Count > 0)
            {
                var current = stack.Pop();

                //First different thing - we check if we already know price of path from here, and add it if we do.
                if (memo.TryGetValue(current, out var memoizedValue))
                {
                    //And we update memo for current path point.
                    DFSAll_UpdateMemo(current, memoizedValue);
                    paths += memoizedValue;
                    continue;
                }

                //And if we found out target we update path count (+ update memo)
                if (current == targetNode)
                {
                    DFSAll_UpdateMemo(current, 1);
                    paths++;
                    continue;
                }

                //And typically - we add all next possiblities into stack. But we remember if we added any...
                var pushed = false;
                foreach (var connected in current.ConnectedNodes)
                {
                    parents[connected] = current;
                    stack.Push(connected);
                    pushed = true;
                }

                //... because if we did not, we memoize to not repeat useless paths too
                if (!pushed)
                {
                    DFSAll_UpdateMemo(current, 0);
                }
            }

            return paths;
        }
    }
}
