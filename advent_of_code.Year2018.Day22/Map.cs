namespace advent_of_code.Year2018.Day22
{
    internal class Map
    {
        public enum RegionType { Rocky = 0, Wet = 1, Narrow = 2 };

        private static readonly int MagicModulo = 20183;
        private static readonly int MagicX = 16807;
        private static readonly int MagicY = 48271;

        private readonly Dictionary<(int X, int Y), int> ErosionIndices = new Dictionary<(int X, int Y), int>();
        private readonly Dictionary<(int X, int Y), RegionType> RegionTypes = new Dictionary<(int X, int Y), RegionType>();
        private (int X, int Y) _size;


        public int Depth { get; private set; }
        public (int X, int Y) Target { get; private set; }
        public (int X, int Y) Size => _size;


        public Map(int targetX, int targetY, int depth)
        {
            Depth = depth;
            Target = (targetX, targetY);

            Generate(targetX, targetY);
        }





        private static readonly Dictionary<RegionType, List<State.Item>> ValidEquipments = new Dictionary<RegionType, List<State.Item>>()
        {
            {RegionType.Rocky, [State.Item.ClimbingGear, State.Item.Torch] },
            {RegionType.Wet, [State.Item.ClimbingGear, State.Item.None] },
            {RegionType.Narrow, [State.Item.Torch, State.Item.None] },
        };

        public static bool ValidEquipment(RegionType type, State.Item equip)
        {
            return ValidEquipments[type].Contains(equip);
        }

        public static IEnumerable<State.Item> GetValidEquipments(RegionType type, State.Item? except = null)
        {
            foreach (var item in ValidEquipments[type])
            {
                if (except != item) yield return item;
            }
        }


        public RegionType GetRegionTypeAt(int x, int y)
        {
            var grow = false;
            var nX = _size.X;
            var nY = _size.Y;
            if (x > _size.X)
            {
                nX = x + _size.X / 2;
                grow = true;
            }
            if (y > _size.Y)
            {
                nY = y + _size.Y / 2;
                grow = true;
            }
            if (grow) Generate(nX, nY);

            return RegionTypes[(x, y)];
        }

        private int GetErosionLevels(int x, int y)
        {
            if (ErosionIndices.TryGetValue((x, y), out var value))
            {
                return value;
            }

            if (x == 0 && y == 0) value = 0;
            else if (x == Target.X && y == Target.Y) value = 0;
            else if (y == 0) value = x * MagicX;
            else if (x == 0) value = y * MagicY;
            else value = ErosionIndices[(x - 1, y)] * ErosionIndices[(x, y - 1)];

            return ErosionIndices[(x, y)] = (value + Depth) % MagicModulo;
        }

        public void Generate(int width, int height)
        {
            if (width > _size.X) _size.X = width;
            if (height > _size.Y) _size.Y = height;
            for (var y = 0; y <= _size.Y; y++)
            {
                for (var x = 0; x <= _size.X; x++)
                {
                    if (!RegionTypes.ContainsKey((x, y)))
                    {
                        RegionTypes.Add((x,y),(RegionType)(GetErosionLevels(x, y)%3));
                    }
                }
            }
        }
    }
}
