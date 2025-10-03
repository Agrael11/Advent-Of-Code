namespace advent_of_code.Year2019.Day14
{
    internal class Common
    {
        private static Dictionary<string, Recipe> recipeMap = new Dictionary<string, Recipe>();

        public static void ParseRecipes(IEnumerable<string> input)
        {
            recipeMap = input.Select(Recipe.ParseRecipe).ToDictionary(t=>t.ChemicalName, t=>t);
        }
        
        public static long CraftFuel(long amount)
        {
            var chemicalsInPosession = new Dictionary<string, long>() { };
            var toCraftAmounts = new Dictionary<string, long>() { };
            var toCraftChemicals = new Queue<string>();
            toCraftAmounts["FUEL"] = amount;
            toCraftChemicals.Enqueue(("FUEL"));

            var ore = 0L;

            while (toCraftChemicals.Count > 0)
            {
                var chemicalName = toCraftChemicals.Dequeue();
                var needed = toCraftAmounts[chemicalName];
                toCraftAmounts[chemicalName] = 0;

                if (chemicalsInPosession.TryGetValue(chemicalName, out var extra))
                {
                    if (extra >= needed)
                    {
                        chemicalsInPosession[chemicalName] -= needed;
                        continue;
                    }
                    else
                    {
                        needed -= extra;
                        chemicalsInPosession[chemicalName] = 0;
                    }
                }

                var recipe = recipeMap[chemicalName];
                var (neededIngridients, leftOver) = recipe.Craft(needed);

                if (leftOver > 0)
                {
                    if (chemicalsInPosession.ContainsKey(chemicalName))
                    {
                        chemicalsInPosession[chemicalName] += leftOver;
                    }
                    else
                    {
                        chemicalsInPosession[chemicalName] = leftOver;
                    }
                }

                foreach (var result in neededIngridients)
                {
                    if (result.chemicalName == "ORE")
                    {
                        ore += result.quanity;
                    }
                    else
                    {
                        if (toCraftChemicals.Contains(result.chemicalName))
                        {
                            toCraftAmounts[result.chemicalName] += result.quanity;
                            continue;
                        }
                        else
                        {
                            toCraftChemicals.Enqueue(result.chemicalName);
                            toCraftAmounts[result.chemicalName] = result.quanity;
                        }
                    }
                }
            }

            return ore;
        }
    }
}
