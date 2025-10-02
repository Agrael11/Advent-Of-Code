using IntMachine;

namespace advent_of_code.Year2019.Day09
{
    internal class Common
    {
        public static bool TryGetOutput(long[] program, int inputCode, out long[] results)
        {
            var outputs = new List<long>();
            var machine = new Machine
            {
                RAM = (Memory)program
            };

            machine.PushInput(inputCode);

            machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);

            while (machine.OutputAvailable())
            {
                if (machine.TryPopOutput(out var output) && output is not null)
                {
                    outputs.Add(output.Value);
                }
            }

            results = outputs.ToArray();

            return (outputs.Count == 1);
        }
    }
}
