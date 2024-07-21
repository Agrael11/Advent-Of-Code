using advent_of_code.Helpers;

namespace advent_of_code.Year2018.Day11
{
    internal class Common
    {
        private static int serialNumber = -1;
        private static readonly Dictionary<(int x, int y), long> CellPowerMemory = new Dictionary<(int x, int y), long>();
        private static readonly Dictionary<(int x, int y), long> SumMemory = new Dictionary<(int x, int y), long>();

        public static void Setup(int serial, bool precalc = false)
        {
            serialNumber = serial;
            CellPowerMemory.Clear();
            SumMemory.Clear();
            if (precalc)
            {
                for (var x = 1; x <= 300; x++)
                {
                    for (var y = 1; y <= 300; y++)
                    {
                        GetSumAt(x, y);
                    }
                }
            }
        }

        public static (int x, int y, long power) GetHighestAtSize(int size)
        {
            (int x, int y) largestCoords = (-1, -1);
            var largestTotalPower = long.MinValue;

            for (var x = 1; x <= 300 - size; x++)
            {
                for (var y = 1; y <= 300 - size; y++)
                {
                    var total = GetSumAtRegion(x, y, size);
                    if (total > largestTotalPower)
                    {
                        largestTotalPower = total;
                        largestCoords = (x, y);
                    }
                }
            }

            return (largestCoords.x, largestCoords.y, largestTotalPower);
        }

        public static long GetSumAtRegion(int x, int y, int size)
        {
            (int x, int y) Apoint = (x - 1, y - 1);
            (int x, int y) Bpoint = (x + size - 1, y - 1);
            (int x, int y) Cpoint = (x - 1, y + size - 1);
            (int x, int y) Dpoint = (x + size - 1, y + size - 1);
            return GetSumAt(Dpoint.x, Dpoint.y) - GetSumAt(Bpoint.x, Bpoint.y) - GetSumAt(Cpoint.x, Cpoint.y) + GetSumAt(Apoint.x, Apoint.y);
        }

        public static long GetSumAt(int x, int y)
        {
            if (x <= 0 || y <= 0) return 0;
            if (SumMemory.TryGetValue((x,y), out var sum))
            {
                return sum;
            }
            sum = GetCellPowerLevel(x, y) + GetSumAt(x,y-1) + GetSumAt(x-1,y) - GetSumAt(x-1,y-1);
            SumMemory[(x,y)] = sum;
            return sum;
        }

        private static long GetCellPowerLevel(int x, int y)
        {
            if (x <= 0 || y <= 0) return 0;
            if (CellPowerMemory.TryGetValue((x, y), out var powerLevel))
            {
                return powerLevel;
            }

            var rackID = (x + 10);
            powerLevel = ((rackID * y + serialNumber) * rackID / 100) % 10 - 5;
            CellPowerMemory[(x, y)] = powerLevel;
            return powerLevel;
        }
    }
}
