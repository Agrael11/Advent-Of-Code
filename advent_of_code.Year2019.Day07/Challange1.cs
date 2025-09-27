using advent_of_code.Helpers;
using IntMachine;

namespace advent_of_code.Year2019.Day07
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(",").Select(int.Parse).ToArray();

            List<int> options = [0, 1, 2, 3, 4];

            var maximum = int.MinValue;

            foreach (var permutation in options.YieldPermutate())
            {
                var strength = RunMachines(input, permutation);
                if (strength > maximum)
                {
                    maximum = strength;
                }
            }

            return maximum;
        }

        private static int RunMachines(int[] ram, List<int> signal)
        {
            var input = 0;

            for (var i = 0; i < 5; i++)
            {
                input = RunMachine((Memory)ram, input, signal[i]);
            }

            return input;
        }

        private static int RunMachine(Memory ram, int input, int setting)
        {
            var machine = new Machine()
            {
                RAM = ram
            };

            machine.PushInput(setting);
            machine.PushInput(input);

            var result = machine.Run([1,2,3,4,5,6,7,8,99]);
            
            if (result != Machine.RunResult.Exit)
            {
                throw new Exception($"Incorrect machine exit state: {Enum.GetName(result)}");
            }
            
            if (!machine.OutputAvailable() || !machine.TryPopOutput(out var output) || output is null)
            {
                throw new Exception("No or incorrect output type");
            }
            
            return output.Value;
        }
    }
}