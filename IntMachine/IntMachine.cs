namespace IntMachine
{
    public class Machine
    {
        public enum RunResult { Okay, Exit, IndexOutOfRange, WrongOpcode, UnsupportedOpcode }

        private readonly Dictionary<int, Func<RunResult>> OpcodeMap = new Dictionary<int, Func<RunResult>>();

        public Memory RAM;
        public int PC { get; set; }

        public Machine()
        {
            RAM = new Memory(0);
            PC = 0;
            OpcodeMap.Add(1, OpcodeAdd);
            OpcodeMap.Add(2, OpcodeMul);
            OpcodeMap.Add(99, OpcodeExit);
        }


        public RunResult Run(params List<int> supportedOpcodes)
        {
            while (true)
            {
                var result = Step(supportedOpcodes);
                if (result != RunResult.Okay) return result;
            }
        }

        public RunResult Step(params List<int> supportedOpcodes)
        {
            if (!RAM.TryRead(PC, out var opcode) || opcode is null) return RunResult.IndexOutOfRange;
            if (!supportedOpcodes.Contains(opcode.Value)) return RunResult.UnsupportedOpcode;
            if (!OpcodeMap.TryGetValue(opcode.Value, out var fuction) || fuction is null) return RunResult.WrongOpcode;
            return fuction.Invoke();
        }

        private RunResult OpcodeAdd()
        {
            var memAddr = PC;
            PC += 4;
            if (!RAM.TryReadIndirect(memAddr+1, out var value1) ||
                !RAM.TryReadIndirect(memAddr+2, out var value2) ||
                value1 is null || value2 is null ||
                !RAM.TryWriteIndirect(memAddr+3, value1.Value + value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeMul()
        {
            var memAddr = PC;
            PC += 4;
            if (!RAM.TryReadIndirect(memAddr + 1, out var value1) ||
                !RAM.TryReadIndirect(memAddr + 2, out var value2) ||
                value1 is null || value2 is null ||
                !RAM.TryWriteIndirect(memAddr + 3, value1.Value * value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeExit()
        {
            return RunResult.Exit;
        }
    }
}
