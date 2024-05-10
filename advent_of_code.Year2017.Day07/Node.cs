using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day07
{
    internal class Node (string name, int weigth)
    {
        public string Name { get; set; } = name;
        public int Weight { get; set; } = weigth;
        public readonly List<Node> Holding = new List<Node>();
        public readonly List<Node> HeldBy = new List<Node>();
        
        public int TotalWeight
        {
            get
            {
                var totalWeight = Weight;
                foreach (var heldNode in Holding)
                {
                    totalWeight += heldNode.TotalWeight;
                }
                return totalWeight;
            }
        }

        public static List<Node> ParseInput(string[] input)
        {
            var nodes = new Dictionary<string, Node>();

            foreach (var tower in input)
            {
                var towerSplitSide = tower.Split("->");
                var towerName = towerSplitSide[0].Split(' ')[0];
                var towerWeight = int.Parse(towerSplitSide[0].Split(' ')[1].TrimStart('(').TrimEnd(')'));
                if (nodes.TryGetValue(towerName, out var node)) node.Weight = towerWeight;
                else nodes.Add(towerName, new Node(towerName, towerWeight));

                if (towerSplitSide.Length == 2)
                {
                    var subtowers = towerSplitSide[1].Split(',');
                    foreach (var subtower in subtowers)
                    {
                        var subtowerName = subtower.Trim(' ');

                        if (!nodes.TryGetValue(subtowerName, out node))
                        {
                            node = new Node(subtowerName, -1);
                            nodes.Add(subtowerName, node);
                        }

                        nodes[towerName].Holding.Add(node);
                        node.HeldBy.Add(nodes[towerName]);
                    }
                }
            }

            return nodes.Values.ToList();
        }
    }
}
