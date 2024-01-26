using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day19
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var inputMolecule = input[^1];
            var replacements = new Dictionary<string, List<string>>();

            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line)) break;
                var original = line.Split("=>")[0].Trim();
                var replacement = line.Split("=>")[1].Trim();
                if (!replacements.TryAdd(original, [replacement]))
                {
                    replacements[original].Add(replacement);
                }
            }

            var newMolecules = new HashSet<string>();

            foreach (var key in replacements.Keys)
            {
                foreach (var index in inputMolecule.IndexesOf(key))
                {
                    foreach (var replacement in replacements[key])
                    {
                        var newMolecule = inputMolecule[..index] + replacement + inputMolecule[(index + key.Length)..];
                        newMolecules.Add(newMolecule);
                    }
                }
            }

            return newMolecules.Count;
        }
    }
}