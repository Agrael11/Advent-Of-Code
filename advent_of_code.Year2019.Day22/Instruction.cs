namespace advent_of_code.Year2019.Day22
{
    public enum InstructionType { Deal, Cut, DealWithIncrement}
    internal class Instruction
    {
        public InstructionType Type { get; }
        public int Value { get; }
        public Instruction(InstructionType type, int value = 0)
        {
            Type = type;
            Value = value;
        }

        public Instruction(string instructionLine)
        {
            if (instructionLine.StartsWith("deal into"))
            {
                Type = InstructionType.Deal;
            }
            else if (instructionLine.StartsWith("cut"))
            {
                Type = InstructionType.Cut;
                var parts = instructionLine.Split(' ');
                Value = int.Parse(parts[1]);
            }
            else if (instructionLine.StartsWith("deal with increment"))
            {
                Type = InstructionType.DealWithIncrement;
                var parts = instructionLine.Split(' ');
                Value = int.Parse(parts[3]);
            }
            else
            {
                throw new ArgumentException("Invalid instruction line: " + instructionLine);
            }
        }
    }
}
