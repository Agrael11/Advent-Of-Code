namespace advent_of_code.Year2015.Day05
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            char[] vowels = ['a', 'e', 'i', 'o', 'u'];
            string[] forbidden = ["ab", "cd", "pq", "xy"];

            var niceWords = 0;

            foreach (var line in input)
            {
                var last = '\0';
                var foundVowels = 0;
                var foundTwo = false;
                var nice = true;

                for (var i = 0; i < line.Length; i++)
                {
                    if (forbidden.Contains(last.ToString() + line[i]))
                    {
                        nice = false;
                        break;
                    }

                    if (!foundTwo && last == line[i])
                    {
                        foundTwo = true;
                    }

                    if (vowels.Contains(line[i])) foundVowels++;

                    if (nice && foundTwo && foundVowels >= 3)
                    {
                        niceWords++;
                        break;
                    }

                    last = line[i];
                }
            }

            return niceWords;
        }
    }
}
