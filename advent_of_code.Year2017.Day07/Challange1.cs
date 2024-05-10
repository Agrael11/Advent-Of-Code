using System.ComponentModel.DataAnnotations;

namespace advent_of_code.Year2017.Day07
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var nodes = Node.ParseInput(input);

            foreach (var node in nodes)
            {
                if (node.HeldBy.Count == 0) return node.Name;
            }

            return "NoNodeFoundError";
        }
    }
}