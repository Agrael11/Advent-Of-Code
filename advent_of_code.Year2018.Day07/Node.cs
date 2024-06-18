using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day07
{
    internal class Node(char id)
    {
        public char ID { get; private set; } = id;

        public List<char> Required { get; private set; } = new List<char>();
        public List<char> Dependants { get; private set; } = new List<char>();

        public void AddDependancy(char node)
        {
            if (!Required.Contains(node))
            {
                Required.Add(node);
            }
        }

        public void RemoveDependency(char node)
        {
            Required.Remove(node);
        }

        public void AddDependant(char node)
        {
            if (!Dependants.Contains(node))
            {
                Dependants.Add(node);
            }
        }

        public bool NoDependencies()
        {
            return Required.Count == 0;
        }

        public bool IsNotRequired()
        {
            return Dependants.Count == 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Node n) return false;
            return ID == n.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
