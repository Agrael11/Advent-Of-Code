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
    }
}
