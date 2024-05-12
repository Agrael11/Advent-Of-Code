using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day12
{
    internal class Node(int id)
    {
        public int ID { get; private set; } = id;
        public List<int> ConnectsTo { get; private set; } = new List<int>();

        public static Dictionary<int, Node> Parse(string[] input)
        {
            var nodes = new Dictionary<int, Node>();
            
            foreach (var nodeInformation in input)
            {
                var nodeData = nodeInformation.Split("<->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(nodeData[0]);
                var node = new Node(id);
                if (nodeData.Length == 2)
                {
                    foreach (var subnode in nodeData[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    {
                        node.ConnectsTo.Add(int.Parse(subnode));
                    }
                }
                nodes.Add(id, node);
            }

            return nodes;
        }
    }
}
