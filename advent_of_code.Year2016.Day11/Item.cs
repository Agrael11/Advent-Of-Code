using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day11
{
    public class Item(Item.ItemTypes itemType, string itemSubtype)
    {
        public enum ItemTypes { Generator, Microchip };
        
        public ItemTypes ItemType = itemType;
        public string ItemSubtype = itemSubtype;

        public bool Compatible(Item item2)
        {
            if (ItemType == item2.ItemType) return true;

            if (ItemSubtype == item2.ItemSubtype) return true;

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Item item) return false;
            return (item.ItemType == ItemType && item.ItemSubtype == ItemSubtype);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemType, ItemSubtype);
        }

        public override string ToString()
        {
            return "the " + ItemSubtype + (ItemType == ItemTypes.Generator?" generator":"-compatible microchip");
        }

        public static bool operator ==(Item left, Item right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !(left == right);
        }
    }
}
