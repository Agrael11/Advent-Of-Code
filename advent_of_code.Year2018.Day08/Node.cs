using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day08
{
    internal class Node
    {
        public readonly List<Node> Nodes = new List<Node>();
        public readonly List<int> Metadata = new List<int>();
        public readonly List<int> Data = new List<int>();
        public int MetadataSum => Metadata.Sum() + Nodes.Sum(n => n.MetadataSum);
        public int Value
        {
            get
            {
                if (Nodes.Count == 0)
                {
                    return Metadata.Sum();
                }
                else
                {
                    var sum = 0;

                    foreach(var indexer in Metadata)
                    {
                        if (indexer > Nodes.Count || indexer <= 0)
                            continue;

                        sum += Nodes[indexer-1].Value;
                    }

                    return sum;
                }
            }
        }

        public static Node Parse(ref List<int> numbers)
        {
            var position = 0;
            return Parse(ref numbers, ref position);
        }

        public static Node Parse(ref List<int> numbers, ref int position)
        {
            var nodeCount = numbers[position++];
            var metadataCount = numbers[position++];

            var node = new Node();

            for (var i = 0; i < nodeCount; i++)
            {
                node.Nodes.Add(Parse(ref numbers, ref position));
            }

            for (var i = 0; i < metadataCount; i++)
            {
                node.Metadata.Add(numbers[position++]);
            }

            return node;
        }
    }
}
