namespace advent_of_code.Year2015.Day22
{
    public class Magic(string itemName, ushort manaCost, short damage, ushort armor, ushort manaRecharge, short hpRecharge, ushort lasts)
    {
        public string ItemName { get; } = itemName;
        public ushort ManaCost { get; } = manaCost;
        public short Damage { get; } = damage;
        public ushort Armor { get; } = armor;
        public short HPRecharge { get; } = hpRecharge;
        public ushort ManaRecharge { get; } = manaRecharge;
        public ushort Lasts { get; } = lasts;

        public bool Instant => Lasts == 1;

        public override bool Equals(object? obj)
        {
            return obj is Magic magic &&
                   ItemName == magic.ItemName &&
                   Lasts == magic.Lasts;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemName, Lasts);
        }
    }
}
