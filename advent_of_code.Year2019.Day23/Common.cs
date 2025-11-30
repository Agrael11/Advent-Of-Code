using IntMachine;

namespace advent_of_code.Year2019.Day23
{
    internal class Common
    {
        internal static bool TryGetMachineOutputs(Machine machine, out int target, out long X, out long Y)
        {
            target = -1;
            X = -1;
            Y = -1;
            if (!machine.TryPopOutput(out var outputValue1) || outputValue1 is null) return false;
            if (!machine.TryPopOutput(out var outputValue2) || outputValue2 is null) return false;
            if (!machine.TryPopOutput(out var outputValue3) || outputValue3 is null) return false;
            target = (int)outputValue1.Value;
            X = outputValue2.Value;
            Y = outputValue3.Value;
            return true;
        }
    }
}
