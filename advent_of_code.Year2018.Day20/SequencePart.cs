namespace advent_of_code.Year2018.Day20
{
    internal class SequencePart (string mainString, Sequence? parent)
    {
        private readonly List<Sequence> options = new List<Sequence>();

        public Sequence? Parent { get; init; } = parent;
        public string MainString { get; init; } = mainString;
        public int OptionsCount => options.Count;

        public void AddOption(Sequence sequence)
        {
            options.Add(sequence);
        }
        public Sequence GetOption(int index)
        {
            return options[index];
        }
    }
}
