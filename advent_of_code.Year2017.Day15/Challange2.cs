﻿namespace advent_of_code.Year2017.Day15
{
    public static class Challange2
    {
        private static readonly uint GenAFactor = 16_807;
        private static readonly uint GenBFactor = 48_271;
        private static readonly uint GenAPicky = 4;
        private static readonly uint GenBPicky = 8;
        private static readonly uint Divisor = 2_147_483_647;
        private static readonly uint Count = 5_000_000;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var GenAValue = ulong.Parse(input[0].Split(' ')[^1]);
            var GenBValue = ulong.Parse(input[1].Split(' ')[^1]);

            var found = 0;

            for (var i = 0; i < Count; i++)
            {
                found += Match(ref GenAValue, ref GenBValue) ? 1 : 0;
            }

            return found;
        }

        private static bool Match(ref ulong A, ref ulong B)
        {
            unchecked
            {
                do
                {
                    A = (A * GenAFactor) % Divisor;
                } while (A % GenAPicky != 0);

                do
                {
                    B = (B * GenBFactor) % Divisor;
                } while (B % GenBPicky != 0);
            }

            return (A & 0xFFFF) == (B & 0xFFFF);
        }
    }
}