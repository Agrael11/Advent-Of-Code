using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day11
{
    public class FloorState
    {
        public HashSet<Item> Items = new HashSet<Item>();

        public FloorState Clone()
        {
            var newItems = new HashSet<Item>();
            foreach (var item in Items)
            {
                newItems.Add(item);
            }
            return new FloorState() { Items = newItems };
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public bool FloorValid()
        {
            var active = false;
            var inactive = false;
            foreach (var item in Items)
            {
                if (item.ItemType == Item.ItemTypes.Microchip)
                {
                    var found = false;
                    foreach (var item2 in Items)
                    {
                        if (item.ItemSubtype == item2.ItemSubtype && item2.ItemType == Item.ItemTypes.Generator)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found) active = true;
                    else inactive = true;
                }
            }
            if (active && inactive) return false;
            return true;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not FloorState other) return false;
            return other.Items.SetEquals(Items);
        }

        public override int GetHashCode()
        {
            var hashCode = 17;

            var sortedItems = Items.OrderBy(item => item.GetHashCode());
            
            foreach (var item in sortedItems)
            {
                hashCode = hashCode*31 + item.GetHashCode();
            }

            return hashCode;
        }

        public override string ToString()
        {
            return "This floor contains " + string.Join(',', Items);
        }
    }
}
