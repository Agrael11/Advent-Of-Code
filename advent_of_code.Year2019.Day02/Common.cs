namespace advent_of_code.Year2019.Day02
{
    internal class Common
    {
        public static bool TryRunMachine(int noun, int verb, int[] input, out int result)
        {
            var machine = new IntMachine.Machine
            {
                RAM = (IntMachine.Memory)input
            };

            machine.RAM.TryWrite(1, noun);
            machine.RAM.TryWrite(2, verb);

            if (machine.Run([1, 2, 99]) != IntMachine.Machine.RunResult.Exit)
            {
                result = 0;
                return false;
            }

            if (!machine.RAM.TryRead(0, out var tmpresult) || !tmpresult.HasValue)
            {
                result = 0;
                return false;
            }

            result = tmpresult.Value;
            return true;
        }
    }
}
