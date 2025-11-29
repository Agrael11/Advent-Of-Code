namespace advent_of_code.Year2019.Day22
{
    public static class Challange1
    {
        private static readonly long targetCard = 2019;
        private static readonly long deckSize = 10007;

        public static long DoChallange(string inputData)
        {
            //inputData = "cut 6\r\ndeal with increment 7\r\ndeal into new stack";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            var instructions = input.Select(t=>new Instruction(t));

            var position = targetCard;

            foreach (var instruction in instructions)
            {
                switch (instruction.Type)
                {
                    case InstructionType.Cut:
                        position -= instruction.Value;
                        while (position < 0) position += deckSize;
                        position %= deckSize;
                        break;
                    case InstructionType.DealWithIncrement:
                        position = (position * instruction.Value) % deckSize;
                        break;
                    case InstructionType.Deal:
                        position = deckSize - 1 - position;
                        break;
                }
            }
            return position;
        }

        private static void PrintList(DeckList list)
        {
            Visualizers.AOConsole.WriteLine("Current deck:");
            foreach (var item in list)
            {
                Visualizers.AOConsole.Write(item + ", ");
            }
            Visualizers.AOConsole.WriteLine("");
        }
    }
}