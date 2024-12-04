namespace advent_of_code.Year2024.Day04
{
    internal static class Common
    {
        public static int Width { get; private set; } = 0;
        public static int Height { get; private set; } = 0;

        private static char[][] letters = Array.Empty<char[]>();

        //Contains vertical strings and 
        public static string[] Letters
        {
            set
            {
                letters = value.Select(x => x.ToCharArray()).ToArray(); //Just converts String[] to char[][]
                Height = letters.Length;
                if (Height > 0) Width = value[0].Length;
            }
        }

        public static char GetLetterAt(int x, int y)
        {
            //If out of bounds we'll return empty character
            if (x < 0 | x >= Width | y < 0 | y >= Height)
                return '\0';

            return letters[y][x];
        }

        
    }
}
