using System.IO.Pipelines;

namespace IntMachine
{
    public class Machine
    {
        public enum RunResult { Okay, Exit, IndexOutOfRange, WrongOpcode, UnsupportedOpcode, WaitingForInput }

        private readonly Queue<int> InputQueue = new Queue<int>();
        private readonly Queue<int> OutputQueue = new Queue<int>();

        private delegate RunResult OpcodeFunction(int arg1mode, int arg2mode, int arg3mode);

        public static (int arg1mode, int arg2mode, int arg3mode, int opcode) ParseOpcode(int data)
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

        public void PushInput(int value)
        {
            InputQueue.Enqueue(value);
        }
        private void PushOutput(int value)
        {
            OutputQueue.Enqueue(value);
        }

        public bool OutputAvailable()
        {
            return OutputQueue.Count > 0;
        }

        public bool TryPopOutput(out int? output)
        {
            output = null;
            if (OutputQueue.Count == 0) return false;
            output = OutputQueue.Dequeue();
            return true;
        }

        private bool TryPopInput(out int? input)
        {
            input = null;
            if (InputQueue.Count == 0) return false;
            input = InputQueue.Dequeue();
            return true;
        }

        private readonly Dictionary<int, OpcodeFunction> OpcodeMap = new Dictionary<int, OpcodeFunction>();

        public Memory RAM;
        public int PC { get; set; }

        public Machine()
        {
            RAM = new Memory(0);
            PC = 0;
            OpcodeMap.Add(1, OpcodeAdd);
            OpcodeMap.Add(2, OpcodeMul);
            OpcodeMap.Add(3, OpcodeInput);
            OpcodeMap.Add(4, OpcodeOutput);
            OpcodeMap.Add(5, OpcodeJNZ);
            OpcodeMap.Add(6, OpcodeJZ);
            OpcodeMap.Add(7, OpcodeCMPL);
            OpcodeMap.Add(8, OpcodeCMPE);
            OpcodeMap.Add(99, OpcodeExit);
        }

        private bool ReadOpcodeArg(int memAddr, int mode, out int? result)
        {
            result = null;
            if (!RAM.TryRead(memAddr, out var immediateValue) || immediateValue is null) return false;
            if (mode == 1)
            {
                result = immediateValue;
                return true;
            }
            return RAM.TryRead(immediateValue.Value, out result);
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
            if (!RAM.TryRead(PC, out var value) || value is null) return RunResult.IndexOutOfRange;
            var (arg1mode, arg2mode, arg3mode, opcode) = ParseOpcode(value.Value);
            if (!supportedOpcodes.Contains(opcode)) return RunResult.UnsupportedOpcode;
            if (!OpcodeMap.TryGetValue(opcode, out var fuction) || fuction is null) return RunResult.WrongOpcode;
            return fuction.Invoke(arg1mode, arg2mode, arg3mode);
        }

        private RunResult OpcodeAdd(int arg1mode, int arg2mode, int arg3mode)
        {
            var memAddr = PC;
            PC += 4;
            if (!ReadOpcodeArg(memAddr+1, arg1mode, out var value1) ||
                !ReadOpcodeArg(memAddr+2, arg2mode, out var value2) ||
                value1 is null || value2 is null ||
                !RAM.TryWriteIndirect(memAddr+3, value1.Value + value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeMul(int arg1mode, int arg2mode, int arg3mode)
        {
            var memAddr = PC;
            PC += 4;
            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var value1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var value2) ||
                value1 is null || value2 is null ||
                !RAM.TryWriteIndirect(memAddr + 3, value1.Value * value2.Value))
            {
                return RunResult.IndexOutOfRange;
            }
            return RunResult.Okay;
        }

        private RunResult OpcodeInput(int arg1mode, int arg2mode, int arg3mode)
        {
            if (TryPopInput(out var input) && input is not null)
            {
                var memAddr = PC;
                PC += 2;
                if (!RAM.TryWriteIndirect(memAddr + 1, input.Value)) 
                    return RunResult.IndexOutOfRange;

                return RunResult.Okay;
            }

            return RunResult.WaitingForInput;
        }

        private RunResult OpcodeOutput(int arg1mode, int arg2mode, int arg3mode)
        {
            var memAddr = PC;
            PC += 2;
            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var result) || result is null)
                return RunResult.IndexOutOfRange;

            PushOutput(result.Value);

            return RunResult.Okay;
        }

        private RunResult OpcodeJNZ(int arg1mode, int arg2mode, int arg3mode)
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

        private RunResult OpcodeJZ(int arg1mode, int arg2mode, int arg3mode)
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

        private RunResult OpcodeCMPL(int arg1mode, int arg2mode, int arg3mode)
        {
            var memAddr = PC;
            PC += 4;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var arg1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var arg2) ||
                arg1 is null || arg2 is null ||
                !RAM.TryWriteIndirect(memAddr + 3, ((arg1.Value < arg2.Value) ? 1 : 0)))
            {
                return RunResult.IndexOutOfRange;
            }

            return RunResult.Okay;
        }

        private RunResult OpcodeCMPE(int arg1mode, int arg2mode, int arg3mode)
        {
            var memAddr = PC;
            PC += 4;

            if (!ReadOpcodeArg(memAddr + 1, arg1mode, out var arg1) ||
                !ReadOpcodeArg(memAddr + 2, arg2mode, out var arg2) ||
                arg1 is null || arg2 is null ||
                !RAM.TryWriteIndirect(memAddr + 3, ((arg1.Value == arg2.Value) ? 1 : 0)))
            {
                return RunResult.IndexOutOfRange;
            }

            return RunResult.Okay;
        }

        private RunResult OpcodeExit(int arg1mode, int arg2mode, int arg3mode)
        {
            return RunResult.Exit;
        }
    }
}
