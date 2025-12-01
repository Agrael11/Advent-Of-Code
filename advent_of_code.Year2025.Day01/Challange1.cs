namespace advent_of_code.Year2025.Day01
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Dial starts at 50, password at 0
            var dial = 50;
            var password = 0;


            foreach (var instruction in input)
            {
                //We read the number from instrution, and make it negative if instruction s tarts with "L" (rotate left)
                var modifier = int.Parse(instruction[1..]) % 100;
                if (instruction.StartsWith('L')) modifier *= -1;

                //Normalize the number on dial
                dial = (dial + modifier + 100) % 100;
                //And if it's 0 we increment password
                if (dial == 0) password++;
            }

            return password;
        }
    }
}