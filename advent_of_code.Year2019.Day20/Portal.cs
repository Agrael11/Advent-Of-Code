namespace advent_of_code.Year2019.Day20
{
    internal class Portal ((int X, int Y) Position, (int X, int Y) direction, bool outer, string name)
    {
        public bool Outer { get; } = outer;
        public (int X, int Y) Position { get; } = Position;
        public (int X, int Y) Direction { get; } = direction;
        public (int X, int Y) Target => (Position.X + Direction.X, Position.Y + Direction.Y);
        public string Name { get; } = name;
        public Portal? Other { get; set; } = null;

        public override string ToString()
        {
            return $"{((Outer)?'<':'>')}{Name} @ {Position.X};{Position.Y}" + ((Other is null ) ? "" : $" to {Other.Target.X};{Other.Target.Y}");
        }
    }
}
