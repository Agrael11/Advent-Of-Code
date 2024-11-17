namespace advent_of_code.Year2018.Day20
{
    internal class Node(int x, int y)
    {
        public (int X, int Y) Position { get; private set; } = (x, y);
        public List<(int X, int Y)> ConnectedNodes { get; private set; } = new List<(int X, int Y)> ();
    }
}
