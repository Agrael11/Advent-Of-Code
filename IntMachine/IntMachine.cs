namespace IntMachine
{
    public class Machine
    {
        public enum RunResult { Okay, Exit, IndexOutOfRange, WrongOpcode, UnsupportedOpcode, WaitingForInput }

        private readonly Queue<long> InputQueue = new Queue<long>();
        private readonly Queue<long> OutputQueue = new Queue<long>();

        private long RelativeBase = 0;

        private delegate RunResult OpcodeFunction(long arg1mode, long arg2mode, long arg3mode);

        public Machine Clone()
        {
            var machine = new Machine()
            {
                RAM = RAM.Clone(),
                PC = PC,
                RelativeBase = RelativeBase,
            };
            foreach (var input in InputQueue) machine.InputQueue.Enqueue(input);
            foreach (var output in OutputQueue) machine.OutputQueue.Enqueue(output);
            return machine;
        }

        public static (long arg1mode, long arg2mode, long arg3mode, long opcode) ParseOpcode(long data)
        {
            var opcode = data % 100;
            data /= 100;
            var arg1 = data % 10;
            data /= 10;
            var arg2 = data % 10;
            data /= 10;
            var arg3 = data % 10;
            return (arg1, arg2, arg3, opcode);
        }

        public void PushInput(long value)
        {
            InputQueue.Enqueue(value);
        }
        private void PushOutput(long value)
        {
            OutputQueue.Enqueue(value);
        }

        public bool OutputAvailable()
        {
            return OutputQueue.Count > 0;
        }

        public bool InputAvailable()
        {
            return InputQueue.Count > 0;
        }

        public long? PeekOutput()
        {
            if (OutputQueue.Count == 0) return null;
            return OutputQueue.Peek();
        }

        public bool TryPopOutput(out long? output)
        {
            output = null;
            if (OutputQueue.Count == 0) return false;
            output = OutputQueue.Dequeue();
            return true;
        }

        private bool TryPopInput(out long? input)
        {
            input = null;
            if (InputQueue.Count == 0) return false;
            input = InputQueue.Dequeue();
            return true;
        }

        private readonly Dictionary<long, OpcodeFunction> OpcodeMap = new Dictionary<long, OpcodeFunction>();

        public Memory RAM;
        public long PC { get; set; }

        public List<long>? SupportedOpcodes { get; set; } = null;

        public Machine()
        {
            RAM = new Memory();
            PC = 0;
            OpcodeMap.Add(1, OpcodeAdd);
            OpcodeMap.Add(2, OpcodeMul);
            OpcodeMap.Add(3, OpcodeInput);
            OpcodeMap.Add(4, OpcodeOutput);
            OpcodeMap.Add(5, OpcodeJNZ);
            OpcodeMap.Add(6, OpcodeJZ);
            OpcodeMap.Add(7, OpcodeCMPL);
            OpcodeMap.Add(8, OpcodeCMPE);
            OpcodeMap.Add(9, OpcodeRBRINC);
            OpcodeMap.Add(99, OpcodeExit);
        }

        public Machine(params List<long> supportedOpcodes) : this()
        {
            SupportedOpcodes = supportedOpcodes;
        }

        private bool ReadOpcodeArg(long memAddr, long mode, out long? result)
        {
            result = null;
            if (!RAM.TryRead(memAddr, out var immediateValue) || immediateValue is null) return false;
            if (mode == 1)
            {
                result = immediateValue;
                return true;
            }
            else if (mode == 2)
            {
                return RAM.TryRead(immediateValue.Value + RelativeBase, out result);
            }
            
            return RAM.TryRead(immediateValue.Value, out result);
        }

        private bool WriteOpcodeArg(long memAddr, long mode, long value)
        {
            if (!RAM.TryRead(memAddr, out var immediateValue) || immediateValue is null) return false;
            if (mode == 1)
            {
                throw new Exception("Wait what? Immidiate mode in write?");
            }
            else if (mode == 2)
            {
                return RAM.TryWrite(immediateValue.Value + RelativeBase, value);
            }
            return RAM.TryWrite(immediateValue.Value, value);
        }

        public RunResult Run()
        {
            while (true)
            {
                var result = Step(SupportedOpcodes);
                if (result != RunResult.Okay) return result;
            }
        }

        public RunResult Run(params List<long> supportedOpcodes)
        {
            while (true)
            {
                var result = Step(supportedOpcodes);
                if (result != RunResult.Okay) return result;
            }
        }

        public RunResult Step(params List<long>? supportedOpcodes)
        {
            if (!RAM.TryRead(PC, out var value) || value is null) return RunResult.IndexOutOfRange;
            var (arg1mode, arg2mode, arg3mode, opcode) = ParseOpcode(value.Value);
            if (supportedOpcodes is not null && !supportedOpcodes.Contains(opcode)) return RunResult.UnsupportedOpcode;
            if (!OpcodeMap.TryGetValue(opcode, out var fuction) || fuction is null) return RunResult.WrongOpcode;
            return fuction.Invoke(arg1mode, arg2mode, arg3mode);
        }

        private RunResult OpcodeAdd(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 4;
            if (!ReadOpcodeArg(memAddr+1, arg1mode, out var value1) ||
                !ReadOpcodeArg(memAddr+2, arg2mode, out var value2) ||
                value1 is null || value2 is null ||
                !WriteOpcodeArg(memAddr+3, arg3mode, value1.Value + value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeMul(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 4;
            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var value1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var value2) ||
                value1 is null || value2 is null ||
                !WriteOpcodeArg(memAddr + 3, arg3mode, value1.Value * value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeInput(long arg1mode, long arg2mode, long arg3mode)
        {
            if (TryPopInput(out var input) && input is not null)
            {
                var memAddr = PC;
                PC += 2;
                if (!WriteOpcodeArg(memAddr + 1, arg1mode, input.Value)) 
                    return RunResult.IndexOutOfRange;

                return RunResult.Okay;
            }

            return RunResult.WaitingForInput;
        }

        private RunResult OpcodeOutput(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 2;
            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var result) || result is null)
                return RunResult.IndexOutOfRange;

            PushOutput(result.Value);

            return RunResult.Okay;
        }

        private RunResult OpcodeJNZ(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 3;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var condition) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var target) ||
                condition is null || target is null)
            {
                return RunResult.IndexOutOfRange;
            }

            if (condition != 0) PC = target.Value;

            return RunResult.Okay;
        }

        private RunResult OpcodeJZ(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 3;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var condition) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var target) ||
                condition is null || target is null)
            {
                return RunResult.IndexOutOfRange;
            }

            if (condition == 0) PC = target.Value;

            return RunResult.Okay;
        }

        private RunResult OpcodeCMPL(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 4;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var arg1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var arg2) ||
                arg1 is null || arg2 is null ||
                !WriteOpcodeArg(memAddr + 3, arg3mode, ((arg1.Value < arg2.Value) ? 1 : 0)))
            {
                return RunResult.IndexOutOfRange;
            }

            return RunResult.Okay;
        }

        private RunResult OpcodeCMPE(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 4;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var arg1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var arg2) ||
                arg1 is null || arg2 is null ||
                !WriteOpcodeArg(memAddr + 3, arg3mode, (arg1.Value == arg2.Value) ? 1 : 0))
            {
                return RunResult.IndexOutOfRange;
            }

            return RunResult.Okay;
        }

        private RunResult OpcodeRBRINC(long arg1mode, long arg2mode, long arg3mode)
        {
            var memAddr = PC;
            PC += 2;
            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var arg1) || arg1 is null)
            {
                return RunResult.IndexOutOfRange;
            }
            RelativeBase += arg1.Value;
            return RunResult.Okay;
        }

        private RunResult OpcodeExit(long arg1mode, long arg2mode, long arg3mode)
        {
            return RunResult.Exit;
        }
    }
}
