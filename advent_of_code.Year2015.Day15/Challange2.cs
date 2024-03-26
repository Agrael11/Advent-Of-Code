namespace advent_of_code.Year2015.Day15
{
    public static class Challange2
    {
        private readonly static List<Ingredient> ingredients = new List<Ingredient>();

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (var line in input)
            {
                ingredients.Add(new Ingredient(line.Split(':')[1]));
            }

            var bestScore =GetBestScore (new List<int>());

            ingredients.Clear();

            return bestScore;
        }

        private static long GetBestScore(List<int> spoons)
        {
            if (spoons.Count == ingredients.Count - 1)
            {
                var totalSpoons = spoons.Sum();

                spoons.Add(100 - totalSpoons);

                var capacity = 0;
                var calories = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;

                for (var i = 0; i < spoons.Count; i++)
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
                var totalSpoons = spoons.Sum();

                var bestScore = long.MinValue;

                for (var i = 1; i < 100 - totalSpoons - (ingredients.Count - spoons.Count); i++)
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
