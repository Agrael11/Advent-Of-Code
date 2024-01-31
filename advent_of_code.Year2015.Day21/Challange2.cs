namespace advent_of_code.Year2015.Day21
{
    public static class Challange2
    {
        private static int BossHP = 0;
        private static int BossDamage = 0;
        private static int BossDefense = 0;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            foreach (var line in input)
            {
                var definition = line.Split(':');
                if (definition[0] == "Hit Points") BossHP = int.Parse(definition[1].Trim());
                else if (definition[0] == "Damage") BossDamage = int.Parse(definition[1].Trim());
                else if (definition[0] == "Armor") BossDefense = int.Parse(definition[1].Trim());
            }

            var maxCost = int.MinValue;

            foreach (var weapon in StoreData.Weapons)
            {
                foreach (var armor in StoreData.Armors)
                {
                    foreach (var ring1 in StoreData.Rings)
                    {
                        foreach (var ring2 in StoreData.Rings)
                        {
                            if (ring1.Equals(ring2)) continue;
                            var cost = weapon.Cost + armor.Cost + ring1.Cost + ring2.Cost;
                            if (!Wins(weapon, armor, ring1, ring2)) maxCost = int.Max(cost, maxCost);
                        }
                    }
                }
            }

            return maxCost;
        }

        private static bool Wins(InventoryItem weapon, InventoryItem armor, InventoryItem ring1, InventoryItem ring2)
        {
            var playerDamage = Math.Max(weapon.Damage + ring1.Damage + ring2.Damage - BossDefense, 1);
            var enemyDamage = Math.Max(BossDamage - armor.Armor - ring1.Armor - ring2.Armor, 1);

            return Math.Ceiling((float)BossHP / playerDamage) <= Math.Ceiling(100.0f / enemyDamage);
        }
    }
}