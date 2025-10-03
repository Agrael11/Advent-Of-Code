using IntMachine;

namespace advent_of_code.Year2019.Day13
{
    internal class Common
    {
        public static MachineOutput ReadMachineOutputs(Machine machine)
        {
            machine.TryPopOutput(out var x);
            machine.TryPopOutput(out var y);
            machine.TryPopOutput(out var blockID);
            if (blockID == null || y == null || x == null)
            {
                throw new Exception("Not enough outputs available");
            }
            return new MachineOutput(x.Value, y.Value, blockID.Value);
        }
    }
}
