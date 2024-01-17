namespace advent_of_code.Year2015.Day15
{
    public static class Challange2
    {
        private static List<Ingredient> ingredients = [];

        public static long DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            ingredients = [];

            foreach (string line in input)
            {
                ingredients.Add(new(line.Split(':')[1]));
            }

            return GetBestScore([]);
        }

        private static long GetBestScore(List<int> spoons)
        {
            if (spoons.Count == ingredients.Count - 1)
            {
                int totalSpoons = spoons.Sum();

                spoons.Add(100 - totalSpoons);

                int capacity = 0;
                int calories = 0;
                int durability = 0;
                int flavor = 0;
                int texture = 0;

                for (int i = 0; i < spoons.Count; i++)
                {
                    calories += spoons[i] * ingredients[i].Calories;
                    capacity += spoons[i] * ingredients[i].Capacity;
                    durability += spoons[i] * ingredients[i].Durability;
                    flavor += spoons[i] * ingredients[i].Flavor;
                    texture += spoons[i] * ingredients[i].Texture;
                }
                spoons.RemoveAt(spoons.Count - 1);

                if (calories != 500) return 0;

                capacity = int.Max(capacity, 0);
                durability = int.Max(durability, 0);
                flavor = int.Max(flavor, 0);
                texture = int.Max(texture, 0);

                return capacity * durability * flavor * texture;
            }
            else
            {
                int totalSpoons = spoons.Sum();

                long bestScore = long.MinValue;

                for (int i = 1; i < 100 - totalSpoons - (ingredients.Count - spoons.Count); i++)
                {
                    spoons.Add(i);
                    bestScore = long.Max(bestScore, GetBestScore(spoons));
                    spoons.RemoveAt(spoons.Count - 1);
                }
                return bestScore;
            }
        }
    }
}
