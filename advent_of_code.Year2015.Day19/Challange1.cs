using advent_of_code.Helpers;
using System.Runtime.CompilerServices;

namespace advent_of_code.Year2015.Day19
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            string inputMolecule = input[^1];
            Dictionary<string, List<string>> replacements = [];

            foreach (string line in input)
            {
                if(string.IsNullOrEmpty(line)) break;
                string original = line.Split("=>")[0].Trim();
                string replacement = line.Split("=>")[1].Trim();
                if (!replacements.TryAdd(original, [replacement]))
                {
                    replacements[original].Add(replacement);
                }
            }

            HashSet<string> newMolecules = [];
            
            foreach (string key in replacements.Keys)
            {
                foreach (int index in inputMolecule.IndexesOf(key))
                {
                    foreach (string replacement in replacements[key])
                    {
                        string newMolecule = inputMolecule[..index] + replacement + inputMolecule[(index+key.Length)..];
                        newMolecules.Add(newMolecule);
                    }
                }
            }

            return newMolecules.Count;
        }
    }
}