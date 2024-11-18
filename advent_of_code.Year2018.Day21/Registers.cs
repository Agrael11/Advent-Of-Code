namespace advent_of_code.Year2018.Day21
{
    internal class Registers(int r0, int r1, int r2, int r3, int r4, int r5)
    {
        private readonly int[] Regs = [r0, r1, r2, r3, r4, r5];
        public EventHandler<int>? Register0Read;
        public static readonly int Length = 6;

        public int this[int i, bool direct = false]
        {
            get
            {
                if (i == 0 && !direct)
                {
                    Register0Read?.Invoke(this, Regs[i]);
                }
                return Regs[i];
            }
            set => Regs[i] = value;
        }
    }
}
