namespace advent_of_code.Year2024.Day07
{
    public class Equation
    {
        public long ExpectedResult { get; private set; }
        public long[] Numbers { get; private set; }

        public Equation(string definition)
        {
            var splitDefinition = definition.Split(':');
            ExpectedResult = long.Parse(splitDefinition[0]);
            Numbers = splitDefinition[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
        }

        public override string ToString()
        {
            return $"{ExpectedResult}: {string.Join(' ', Numbers)}";
        }
    }
}