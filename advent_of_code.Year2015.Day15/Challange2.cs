namespace advent_of_code.Year2015.Day15
{
    public static class Challange2
    {
        private static List<Ingridient> ingridients = [];

        public static long DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            ingridients = [];

            foreach (string line in input)
            {
                ingridients.Add(new(line.Split(':')[1]));
            }

            return GetBestScore(0, []);
        }

        private static long GetBestScore(int ingridient, List<int> spoons)
        {
            if (ingridient == ingridients.Count - 1)
            {
                int totalSpoons = 0;
                foreach (int spoon in spoons)
                {
                    totalSpoons += spoon;
                }
                spoons.Add(100 - totalSpoons);

                int capacity = 0;
                int calories = 0;
                int durability = 0;
                int flavor = 0;
                int texture = 0;

                for (int i = 0; i < spoons.Count; i++)
                {
                    calories += spoons[i] * ingridients[i].Calories;
                    capacity += spoons[i] * ingridients[i].Capacity;
                    durability += spoons[i] * ingridients[i].Durability;
                    flavor += spoons[i] * ingridients[i].Flavor;
                    texture += spoons[i] * ingridients[i].Texture;
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
                int totalSpoons = 0;
                foreach (int spoon in spoons)
                {
                    totalSpoons += spoon;
                }
                long bestScore = long.MinValue;

                for (int i = 1; i < 100 - totalSpoons - (ingridients.Count - ingridient); i++)
                {
                    spoons.Add(i);
                    bestScore = long.Max(bestScore, GetBestScore(ingridient + 1, spoons));
                    spoons.RemoveAt(spoons.Count - 1);
                }
                return bestScore;
            }
        }
    }
}
