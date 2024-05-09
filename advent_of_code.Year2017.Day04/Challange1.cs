namespace advent_of_code.Year2017.Day04
{
    public static class Challange1
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
            foreach (var password in passphrase.Split(' '))
            {
                if (passwords.Contains(password)) return false;
                passwords.Add(password); 
            }
            return true;
        }
    }
}