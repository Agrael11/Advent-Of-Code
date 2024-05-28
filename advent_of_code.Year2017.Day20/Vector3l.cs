namespace advent_of_code.Year2017.Day20
{
    internal class Vector3l
    {
        public long X;
        public long Y;
        public long Z;
        
        public Vector3l(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public long GetDistanceFromZero()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
        public void Add(Vector3l second)
        {
            X += second.X;
            Y += second.Y;
            Z += second.Z;
        }
    }
}