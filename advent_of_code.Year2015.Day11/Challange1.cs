namespace advent_of_code.Year2015.Day11
{
    public static class Challange1
    {
        private static string oldPassword = "";
        private static char[] password = Array.Empty<char>();
        private static readonly List<char> forbiddenSymbols = ['i', 'o', 'l'];

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            oldPassword = input[3..];
            password = [input[0], input[1], input[2]];
            var newPassword = GenerateNextSequence((char)(oldPassword[0] - 1));
            if (!IsHigher(oldPassword, newPassword))
            {
                newPassword = GenerateNextSequence(newPassword[0]);
            }
            return new string(password) + newPassword;
        }

        private static bool IsHigher(string a, string b)
        {
            return (string.Compare(a, b) == -1);
        }

        private static void IncrementPassword(int index = -1)
        {
            if (index == -1) index = password.Length - 1;
            if (index < 0) return;
            if (password[index] == 'z')
            {
                password[index] = 'a';
                IncrementPassword(index - 1);
            }
            else
            {
                if (forbiddenSymbols.Contains(++password[index])) password[index]++;
            }
        }

        private static string GenerateNextSequence(char firstCharLast)
        {
            if (firstCharLast == 'x')
            {
                oldPassword = "aaaaa";
                IncrementPassword();
                return "aabcc";
            }
            var c1 = firstCharLast;
            c1++;
            var c2 = c1;
            c2++;
            var c3 = c2;
            c3++;
            while (forbiddenSymbols.Contains(c1) || forbiddenSymbols.Contains(c2) || forbiddenSymbols.Contains(c3))
            {
                c1++;
                c2++;
                c3++;
            }
            return new string([c1, c1, c2, c3, c3]);
        }
    }
}