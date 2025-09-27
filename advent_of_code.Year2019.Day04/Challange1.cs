namespace advent_of_code.Year2019.Day04
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("-").Select(int.Parse).ToArray();

            var validPasswords = 0;

            for (var i = input[0]; i <= input[1]; i++)
            {
                validPasswords += IsValid(i) ? 1 : 0;
            }

            return validPasswords;
        }

        public static bool IsValid(int password)
        {
            var tmpPas = password;
            var lastDigit = tmpPas % 10;
            tmpPas /= 10;
            var twoSame = false;
            while (tmpPas != 0)
            {
                var newLast = tmpPas % 10;
                if (newLast > lastDigit) return false;
                if (newLast == lastDigit) twoSame = true;
                lastDigit = newLast;
                tmpPas /= 10;
            }

            return twoSame;
        }
    }
}