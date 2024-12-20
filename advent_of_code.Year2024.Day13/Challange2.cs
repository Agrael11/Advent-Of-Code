﻿namespace advent_of_code.Year2024.Day13
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var total = 0L;

            for (var i = 0; i < input.Length; i += 4)
            {
                var result = new ClawMachine(input, i).FindCheapestTickets(10000000000000);
                if (result != -1) total += result;
            }

            return total;
        }
    }
}