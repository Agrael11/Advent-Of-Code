using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day19
{
    public static class Challange2
    {
        private readonly static Dictionary<string, List<string>> replacements = new Dictionary<string, List<string>>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var startMolecule = input[^1];

            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line)) break;
                var original = line.Split("=>")[0].Trim();
                var replacement = line.Split("=>")[1].Trim();
                if (!replacements.TryAdd(replacement, [original]))
                {
                    replacements[replacement].Add(original);
                }
            }

            var (type, cost) = PathFinding.DoAStar(startMolecule, (molecule) => molecule == "e", NextMolecules,
                (molecule) => molecule.Count(character => char.IsUpper(character)), 1);

            replacements.Clear();

            return cost;
        }

        public static IEnumerable<(string, int)> NextMolecules(string molecule)
        {
            var newMolecules = new HashSet<(string, int)>();

            foreach (var key in replacements.Keys)
            {
                foreach (var index in molecule.IndexesOf(key))
                {
                    foreach (var replacement in replacements[key])
                    {
                        var newMolecule = molecule[..index] + replacement + molecule[(index + key.Length)..];

                        newMolecules.Add((newMolecule, 1));
                    }
                }
            }

            return newMolecules;
        }
    }
}