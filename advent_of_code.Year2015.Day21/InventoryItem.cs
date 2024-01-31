using System.ComponentModel;
using System.Numerics;

namespace advent_of_code.Year2015.Day21
{
    public class InventoryItem(string itemName, int cost, int damage, int armor)
    {
        public string ItemName = itemName;
        public int Cost { get; set; } = cost;
        public int Damage { get; set; } = damage;
        public int Armor { get; set; } = armor;

        public override int GetHashCode()
        {
            return HashCode.Combine(Cost.GetHashCode(), Damage.GetHashCode(), Armor.GetHashCode());
        }

        public static bool Equals(InventoryItem? item1, InventoryItem? item2)
        {
            return (item1 is null && item2 is null) || (item1 is not null && item1.Equals(item2));
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not InventoryItem) return false;
            var secondItem = (InventoryItem)obj;
            return secondItem.ItemName == ItemName;
        }

        public static bool operator ==(InventoryItem left, InventoryItem right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InventoryItem left, InventoryItem right)
        {
            return !left.Equals(right);
        }
    }
}
