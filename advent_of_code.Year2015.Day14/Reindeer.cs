namespace advent_of_code.Year2015.Day14
{
    internal class Reindeer
    {
        public int Speed { get; }
        public int BurstLength { get; }
        public int RestLength { get; }

        public int Distance { get; set; } = 0;
        public int Points { get; set; } = 0;

        public Reindeer(string definition)
        {
            var defs = definition.Split(' ');
            Speed = int.Parse(defs[3]);
            BurstLength = int.Parse(defs[6]);
            RestLength = int.Parse(defs[13]);
        }
    }
}
