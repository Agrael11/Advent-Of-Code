namespace advent_of_code.Year2017.Day19
{
    public static class Challange1
    {
        private static readonly char forbiddenSpace = ' ';
        private static readonly char turn = '+';

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var y = 0;
            var x = 0;
            for (var i = 0; i < input[0].Length; i++)
            {
                if (input[0][i] == '|')
                {
                    x = i;
                    break;
                }
            }
            var direction = 2;
            var output = "";

            while (true)
            {
                switch (direction)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

                var currentPoint = GetAt(x, y, input);
                output += GetLetter(currentPoint);

                if (currentPoint == forbiddenSpace)
                {
                    break;
                }
                else if (currentPoint == turn)
                {
                    if (direction == 0 || direction == 2)
                    {
                        if (GetAt(x + 1, y, input) != forbiddenSpace) direction = 1;
                        else direction = 3;
                    }
                    else
                    {
                        if (GetAt(x, y - 1, input) != forbiddenSpace) direction = 0;
                        else direction = 2;
                    }
                }
            }

            return output;
        }

        private static char GetAt(int x, int y, string[] data)
        {
            if (x < 0 || y < 0)
            {
                return forbiddenSpace;
            }
            if (y >= data.Length || x >= data[y].Length)
            {
                return forbiddenSpace;
            }
            return data[y][x];
        }

        private static string GetLetter(char character)
        {
            if (character >= 'A' && character <= 'Z') { return character.ToString(); }
            return "";
        }
    }
}