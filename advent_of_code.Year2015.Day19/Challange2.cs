using advent_of_code.Helpers;
using System.Linq;

namespace advent_of_code.Year2015.Day19
{
    public static class Challange2
    {
        private static Dictionary<string, List<string>> replacements = [];

        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            string startMolecule = input[^1];
            replacements = [];

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line)) break;
                string original = line.Split("=>")[0].Trim();
                string replacement = line.Split("=>")[1].Trim();
                if (!replacements.TryAdd(replacement, [original]))
                {
                    replacements[replacement].Add(original);
                }
            }

            var (type, cost) = PathFinding.DoAStar(startMolecule, (molecule) => { return molecule == "e"; }, NextMolecules,
                (molecule) => molecule.Count(character => char.IsUpper(character)), 1);

            return cost;
        }

        public static IEnumerable<(string, int)> NextMolecules(string molecule)
        { 
            HashSet<(string, int)> newMolecules = [];

            foreach (string key in replacements.Keys)
            {
                foreach (int index in molecule.IndexesOf(key))
                {
                    foreach (string replacement in replacements[key])
                    {
                        string newMolecule = molecule[..index] + replacement + molecule[(index + key.Length)..];

                        newMolecules.Add((newMolecule, 1));
                    }
                }
            }

            return newMolecules;
        }
    }
}