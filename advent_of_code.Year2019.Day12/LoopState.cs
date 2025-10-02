using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day12
{
    public class TrippleState
    {
        public LoopState X { get; } = new LoopState();
        public LoopState Y { get; } = new LoopState();
        public LoopState Z { get; } = new LoopState();

        public long LoopLength
        {
            get
            {
                if (!LoopFound) throw new InvalidOperationException("No loop found");
                return MathHelpers.LCM(X.LoopLength, MathHelpers.LCM(Y.LoopLength, Z.LoopLength));
            }
        }

        public bool LoopFound => X.LoopFound && Y.LoopFound && Z.LoopFound;

        public bool Add((Vector3l pos, Vector3l vel) state)
        {
            var xLooped = X.LoopFound || X.Add((state.pos.X, state.vel.X));
            var yLooped = Y.LoopFound || Y.Add((state.pos.Y, state.vel.Y));
            var zLooped = Z.LoopFound || Z.Add((state.pos.Z, state.vel.Z));
            return xLooped && yLooped && zLooped;
        }
    }

    public class LoopState
    {
        public int Pointer1 { get; set; } = 0;
        public int Pointer2 { get; set; } = 1;
        public int LastDifferent { get; set; } = 1;
        public int LoopLength { get; set; } = 0;

        public bool LoopFound => LoopLength > 0;

        public (long pos, long vel) this[int index]
        {
            get
            {
                if (LoopFound)
                {
                    index %= LoopLength;
                }
                return Data[index];
            }
        }

        public List<(long pos, long vel)> Data { get; } = new List<(long pos, long vel)>();

        public bool Add((long pos, long vel) state)
        {
            Data.Add(state);

            if (Data.Count < (Pointer2+1))
            {
                return false;
            }

            if (Data[Pointer1] == Data[Pointer2])
            {
                if (LastDifferent == Pointer1)
                {
                    LoopLength = Pointer1 + 1;
                    return true;
                }
                Pointer1++;
            }
            else
            {
                Pointer1 = 0;
                LastDifferent = Pointer2;
            }
            Pointer2++;

            return false;
        }

    }
}
