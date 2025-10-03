namespace advent_of_code.Year2019.Day14
{
    internal class Recipe(string chemicalName, long craftQuantity,
        params List<(long quanity, string chemicalName)> ingridients)
    {
        public string ChemicalName { get; } = chemicalName;
        public long Quantity { get; } = craftQuantity;
        public List<(long quanity, string chemicalName)> Ingridients { get; } = ingridients;

        public (List<(long quanity, string chemicalName)> ingredients, long leftOver) Craft(long requiredAmount)
        {
            var required = new List<(long quanity, string chemicalName)>();
            var newCount = (long)Math.Ceiling((double)requiredAmount / Quantity);
            foreach (var ingridient in Ingridients)
            {
                required.Add((ingridient.quanity * newCount, ingridient.chemicalName));
            }

            var leftOver = 0L;
            if (requiredAmount % Quantity != 0)
            {
                leftOver = Quantity - (requiredAmount % Quantity);
            }

            return (required, leftOver);
        }

        public static Recipe ParseRecipe(string input)
        {
            var parts = input.Split("=>");
            var ingridients = parts[0].Split(',')
                .Select(ing => ing.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .Select(ing => (long.Parse(ing[0]), ing[1])).ToList();

            var result = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            return new Recipe(result[1], long.Parse(result[0]), ingridients);
        }

        public override string ToString()
        {
            var ingridientsStrs = Ingridients.Select(ing => $"{ing.quanity} {ing.chemicalName}");
            return $"{string.Join(", ", ingridientsStrs)} => {Quantity} {ChemicalName}";
        }
    }
}
