namespace advent_of_code.Year2015.Day22
{
    internal static class MagicAvailable
    {
        public static readonly List<string> Magics = new List<string>
        {
            "Magic Missile", "Drain", "Shield", "Poison", "Recharge"
        };

        public static readonly Dictionary<string, Magic> MagicStore = new Dictionary<string, Magic>()
        {
            {"Magic Missile", new Magic("Magic Missile", 53, 4, 0, 0, 0, 1) },
            {"Drain", new Magic("Drain", 73, 2, 0, 0, 2, 1) },
            {"Shield", new Magic("Shield", 113, 0, 7, 0, 0, 6) },
            {"Poison", new Magic("Poison", 173, 3, 0, 0, 0, 6) },
            {"Recharge", new Magic("Recharge", 229, 0, 0, 101, 0, 5) }
        };
    }
}
