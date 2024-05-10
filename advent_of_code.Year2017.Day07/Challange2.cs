using System.ComponentModel.DataAnnotations;

namespace advent_of_code.Year2017.Day07
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var nodes = Node.ParseInput(input);

            foreach (var node in nodes)
            {
                var balance = NodeBalance(node);
                if (balance != 0) return balance;
            }

            return -1;
        }

        private static int NodeBalance(Node node)
        {
            if (node.Holding.Count < 2) return 0;

            var first = node.Holding[0].TotalWeight;
            var second = node.Holding[1].TotalWeight;
            if (first == second)
            {
                for (var index = 2; index < node.Holding.Count; index++)
                {
                    var holding = node.Holding[index];
                    if (holding.TotalWeight != first)
                    {
                        return holding.Weight + (first - holding.TotalWeight);
                    }
                    else if (holding.TotalWeight != second)
                    {
                        return holding.Weight + (second - holding.TotalWeight);
                    }
                }
            }
            else
            {
                for (var index = 2; index < node.Holding.Count; index++)
                {
                    var holding = node.Holding[index];
                    if (holding.TotalWeight == first && holding.TotalWeight != second)
                    {
                        return node.Holding[1].Weight + first - second;
                    }
                    else if (holding.TotalWeight != first && holding.TotalWeight == second)
                    {
                        return node.Holding[0].Weight + second - first;
                    }

                }
            }
            
            return 0;
        }
    }
}