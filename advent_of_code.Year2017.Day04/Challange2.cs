namespace advent_of_code.Year2017.Day04
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var valid = 0;

            foreach (var passphrase in input)
            {
                if (PassphraseValid(passphrase)) valid++;
            }

            return valid;
        }

        private static bool PassphraseValid(string passphrase)
        {
            var passwords = new HashSet<string>();
            var anagrams = new HashSet<string>();

            foreach (var password in passphrase.Split(' '))
            {
                var anagramArray = password.ToCharArray();
                Array.Sort(anagramArray);
                var anagram = new string(anagramArray);

                if (passwords.Contains(password) || anagrams.Contains(anagram)) return false;
                passwords.Add(password);
                anagrams.Add(anagram);
            }
            return true;
        }
    }
}