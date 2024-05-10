namespace advent_of_code.Year2017.Day08
{
    public static class Challange1
    {
        private static readonly Dictionary<string, int> registers = new Dictionary<string, int>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            registers.Clear();
            
            foreach (var line in input)
            {
                var instruction = line.Split(' ');
                RunInstruction(instruction[0], int.Parse(instruction[2]) * ((instruction[1] == "dec")?-1:1), instruction[4], instruction[5], int.Parse(instruction[6]));
            }

            return registers.OrderBy(t => t.Value).Last().Value;
        }

        public static void RunInstruction(string target, int increment, string conditionSource, string conditionOperand, int conditionComparer)
        {
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
                if (!registers.TryGetValue(target, out var targetValue))
                {
                    targetValue = 0;
                }
                targetValue += increment;
                registers[target] = targetValue;
            }
        }
    }
}