namespace advent_of_code.Year2019.Day19
{
    internal class PCHandler
    {
        private static List<long> _allowedOpcodes = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 99 ];

        private long[] _program;

        public PCHandler(long[] program)
        {
            _program = program;
        }

        public bool IsPointAffected(int x, int y)
        {
            var machine = new IntMachine.Machine(_allowedOpcodes);
            machine.RAM = (IntMachine.Memory)_program;
            machine.PushInput(x);
            machine.PushInput(y);
            var state = machine.Run();
            return (machine.TryPopOutput(out var output) && output == 1);
        }
    }
}
