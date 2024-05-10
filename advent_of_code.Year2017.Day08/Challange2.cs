namespace advent_of_code.Year2017.Day08
{
    public static class Challange2
    {
        private static readonly Dictionary<string, int> registers = new Dictionary<string, int>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            registers.Clear();

            var highest = int.MinValue;

            foreach (var line in input)
            {
                var instruction = line.Split(' ');
                if (RunInstruction(instruction[0], int.Parse(instruction[2]) * ((instruction[1] == "dec") ? -1 : 1), instruction[4], instruction[5], int.Parse(instruction[6]), out var result))
                {
                    if (result > highest) { highest = result; }
                }
            }

            return highest;
        }

        public static bool RunInstruction(string target, int increment, string conditionSource, string conditionOperand, int conditionComparer, out int modifiedValue)
        {
            modifiedValue = 0;
            if (!registers.TryGetValue(conditionSource, out var comparisonSource))
            {
                comparisonSource = 0;
                registers[conditionSource] = comparisonSource;
            }

            var result = conditionOperand switch
            {
                "==" => comparisonSource == conditionComparer,
                "!=" => comparisonSource != conditionComparer,
                "<=" => comparisonSource <= conditionComparer,
                ">=" => comparisonSource >= conditionComparer,
                "<" => comparisonSource < conditionComparer,
                ">" => comparisonSource > conditionComparer,
                _ => throw new Exception("Unknown operand " + conditionOperand),
            };

            if (result)
            {
                if (!registers.TryGetValue(target, out modifiedValue))
                {
                    modifiedValue = 0;
                }
                modifiedValue += increment;
                registers[target] = modifiedValue;
                return true;
            }
            return false;
        }
    }
}