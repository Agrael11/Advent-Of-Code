namespace advent_of_code.Year2019.Day13
{
    internal struct MachineOutput (long x, long y, long value)
    {
        public enum OutputType {
            Tile,
            Score
        }

        public long X = x;
        public long Y = y;
        public long Value = value;
        public readonly OutputType Type => (X == -1 && Y == 0) ? OutputType.Score : OutputType.Tile;
        public readonly long Score => Value;
        public readonly long TileID => Value;
        public readonly bool IsTile()
        {
            return Type == OutputType.Tile;
        }

        public readonly bool IsScore()
        {
            return Type == OutputType.Score;
        }
    }
}
