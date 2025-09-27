﻿using IntMachine;

namespace advent_of_code.Year2019.Day05
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(",").Select(int.Parse).ToArray();
            
            var machine = new Machine
            {
                RAM = (Memory)input
            };

            machine.PushInput(5);
            var result = machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 99]);
            if (result != Machine.RunResult.Exit)
            {
                throw new Exception($"IntMachine error: {Enum.GetName(result)}");
            }

            int? output = null;
            while (machine.OutputAvailable())
            {
                if (!machine.TryPopOutput(out output) || output is null)
                {
                    throw new Exception("Error popping");
                }
            }

            if (output is null)
            {
                throw new Exception("No output found");
            }

            return output.Value;
        }
    }
}