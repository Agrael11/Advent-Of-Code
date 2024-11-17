namespace advent_of_code.Year2018.Day20
{
    internal class Sequence(SequencePart? parent)
    {
        private readonly List<SequencePart> sequenceParts = new List<SequencePart>();

        public SequencePart? Parent { get; init; } = parent;
        public int Option { get; set; }
        public int SequenceLength => sequenceParts.Count;

        public SequencePart GetSequencePart(int index)
        {
            return sequenceParts[index];
        }

        public void AddSequencePart(SequencePart sequencePart)
        {
            sequenceParts.Add(sequencePart);
        }
    }
}