namespace advent_of_code.Year2017.Day20
{
    internal class Vector3i(int x, int y, int z)
    {
        public int X = x;
        public int Y = y;
        public int Z = z;

        public int GetDistanceFromZero()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
        public void Add(Vector3i second)
        {
            X += second.X;
            Y += second.Y;
            Z += second.Z;
        }
    }
}