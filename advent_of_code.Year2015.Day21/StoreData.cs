namespace advent_of_code.Year2015.Day21
{
    internal static class StoreData
    {
        public static readonly List<InventoryItem> Weapons = new List<InventoryItem>()
        {
            new InventoryItem("Dagger", 8, 4, 0),
            new InventoryItem("Shortsword", 10, 5, 0),
            new InventoryItem("Warhammer", 25, 6, 0),
            new InventoryItem("Longsword", 40, 7, 0),
            new InventoryItem("Greataxe", 75, 8, 0)
        };

        public static readonly List<InventoryItem> Armors = new List<InventoryItem>()
        {
            new InventoryItem("No Armor", 0, 0, 0),
            new InventoryItem("Leather", 13, 0, 1),
            new InventoryItem("Chainmail", 31, 0, 2),
            new InventoryItem("Splintmail", 53, 0, 3),
            new InventoryItem("Bandermail", 75, 0, 4),
            new InventoryItem("Platemail", 102, 0, 5)
        };

        public static readonly List<InventoryItem> Rings = new List<InventoryItem>()
        {
            new InventoryItem("No Ring 1", 0, 0, 0),
            new InventoryItem("No Ring 2", 0, 0, 0),
            new InventoryItem("Damage +1", 25, 1, 0),
            new InventoryItem("Damage +2", 50, 2, 0),
            new InventoryItem("Damage +3", 100, 3, 0),
            new InventoryItem("Defense +1", 20, 0, 1),
            new InventoryItem("Defense +2", 40, 0, 2),
            new InventoryItem("Defense +3", 80, 0, 3),
        };
    }
}
