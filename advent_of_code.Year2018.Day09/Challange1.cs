﻿namespace advent_of_code.Year2018.Day09
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(' ');

            var players = int.Parse(input[0]);
            var maxMarble = int.Parse(input[6]);

            return Common.SimulateGame(players, maxMarble);
        }
    }
}