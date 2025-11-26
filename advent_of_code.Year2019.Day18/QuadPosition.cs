namespace advent_of_code.Year2019.Day18
{
    internal struct QuadPosition
    {
        public (int X, int Y)[] Positions;

        public QuadPosition(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            Positions = [ (x1, y1), (x2,y2), (x3,y3), (x4,y4) ];
        }
        public QuadPosition(QuadPosition other)
        {
            Positions = new (int X, int Y)[4];
            for (var i = 0; i < 4; i++)
            {
                Positions[i] = other.Positions[i];
            }
        }

        public readonly (int X, int Y) this[int index]
        {
            get => Positions[index]; 
            set => Positions[index] = value;
        }



        public override readonly int GetHashCode()
        {
            var code = new HashCode();
            foreach (var (x, y) in Positions)
            {
                code.Add(x);
                code.Add(y);
            }
            return code.ToHashCode();
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is not QuadPosition other)
            {
                return false;
            }
            for (var i = 0; i < 4; i++)
            {
                if (Positions[i] != other.Positions[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}