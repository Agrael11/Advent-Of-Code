namespace advent_of_code.Year2025.Day01
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            //We set this up and start same as in part 1
            var dial = 50;
            var password = 0;

            foreach (var line in input)
            {
                var modifier = int.Parse(line[1..]);

                //This time we first check number of times it passes through 0
                if (line.StartsWith('L'))
                {
                    //For going left we use the remaining number to 100 from dial
                    password += ((100 - dial)%100 + modifier) / 100;
                    modifier %= 100;
                    modifier *= -1;
                }
                else
                {
                    //For going left we use the remaining dial itself
                    password += (dial + modifier) / 100;
                    modifier %= 100;
                }

                dial = (dial + modifier + 100) % 100;
            }
            
            return password;
        }
    }
}