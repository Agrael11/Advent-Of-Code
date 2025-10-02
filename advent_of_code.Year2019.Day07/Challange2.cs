using advent_of_code.Helpers;
using IntMachine;

namespace advent_of_code.Year2019.Day07
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","").Split(",").Select(long.Parse).ToArray();
            
            List<int> options = [5, 6, 7, 8, 9];

            var maximum = long.MinValue;

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

        private static long RunMachines(long[] memory, List<int> signal)
        {
            var machines = new List<Func<long, (Machine.RunResult result, long? output)>>();
            
            for (var i = 0; i < 5; i++)
            {
                machines.Add(PrepareMachineRunMethod(memory, signal[i]));
            }

            var input = 0L;

            for (var i = 0; ; i = (i + 1) % 5)
            {
                var (result, output) = machines[i].Invoke(input);

                if (output is null) throw new Exception("Machine returned null");

                if (i == 4 && result == Machine.RunResult.Exit)
                {
                    return output.Value;
                }

                input = output.Value;
            }
        }

        private static Func<long, (Machine.RunResult, long?)> PrepareMachineRunMethod(long[] memory, int signal)
        {
            var machine = new Machine()
            {
                RAM = (Memory)memory
            };
            machine.PushInput(signal);

            return input =>
            {
                machine.PushInput(input);
                var result = machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 99]);
                machine.TryPopOutput(out var output);
                return (result, output);
            };
        }
    }
}