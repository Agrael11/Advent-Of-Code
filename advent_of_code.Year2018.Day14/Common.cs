namespace advent_of_code.Year2018.Day14
{
    internal class Common
    {
        private static readonly List<byte> Recipes = new List<byte>() { 3, 7 };
        private static int _elf1Index = 0;
        private static int _elf2Index = 1;
        public static int RecipesCount => Recipes.Count;

        public static string GetRecipesAsString(int index, int length)
        {
            var result = 0L;
            for (var offset = 0; offset < length; offset++)
            {
                result *= 10;
                result += Recipes[index + offset];
            }

            return result.ToString().PadLeft(length, '0');
        }

        private static int Elf1Index
        {
            get => _elf1Index;
            set => _elf1Index = value % Recipes.Count;
        }

        private static int Elf2Index
        {
            get => _elf2Index;
            set => _elf2Index = value % Recipes.Count;
        }

        public static void Reset()
        {
            Recipes.Clear();
            Recipes.AddRange([3, 7]);
            Elf1Index = 0;
            Elf2Index = 1;
        }
        
        public static int AddRound()
        {
            var recipe1 = Recipes[Elf1Index];
            var recipe2 = Recipes[Elf2Index];

            var recipeWorth = recipe1 + recipe2;
            if (recipeWorth >= 10)
            {
                Recipes.Add((byte)(recipeWorth / 10));
                recipeWorth %= 10;
            }
            Recipes.Add((byte)recipeWorth);

            Elf1Index += (recipe1+1);
            Elf2Index += (recipe2+1);

            return recipe1 + recipe2;
        }
    }
}
