using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day18
{
    internal class DuetCPU
    {
        private readonly long[] Registers = new long[25];
        private long PC = 0;
        private long Sound = -1;

        public Action<long>? RecoveryEvent;

        public long GetRegister(string name)
        {
            if (name.Length != 1) throw new Exception("Invalid register");
            return Registers[name[0] - 'a'];
        }
        public void SetRegister(string name, long value)
        {
            if (name.Length != 1) throw new Exception("Invalid register");
            Registers[name[0] - 'a'] = value;
        }

        public long GetValueOrRegister(string value)
        {
            if (long.TryParse(value, out var number))
            {
                return number;
            }
            return GetRegister(value);
        }

        public void Run(string[] instructions)
        {
            while (PC < instructions.Length && ExecuteInstruction(instructions[PC])) {; }
        }

        public bool ExecuteInstruction(string instruction)
        {
            var splitInstruction = instruction.Split(' ');
            switch (splitInstruction[0])
            {
                case "snd":
                    Sound = GetValueOrRegister(splitInstruction[1]);
                    PC++;
                    break;
                case "set":
                    SetRegister(splitInstruction[1], GetValueOrRegister(splitInstruction[2]));
                    PC++;
                    break;
                case "add":
                    SetRegister(splitInstruction[1], GetRegister(splitInstruction[1]) + GetValueOrRegister(splitInstruction[2]));
                    PC++;
                    break;
                case "mul":
                    SetRegister(splitInstruction[1], GetRegister(splitInstruction[1]) * GetValueOrRegister(splitInstruction[2]));
                    PC++;
                    break;
                case "mod":
                    SetRegister(splitInstruction[1], GetRegister(splitInstruction[1]) % GetValueOrRegister(splitInstruction[2]));
                    PC++;
                    break;
                case "rcv":
                    if (GetValueOrRegister(splitInstruction[1]) != 0)
                    {
                        RecoveryEvent?.Invoke(Sound);
                        return false;
                    }
                    PC++;
                    return true;
                case "jgz":
                    if (GetValueOrRegister(splitInstruction[1]) > 0)
                    {
                        PC += GetValueOrRegister(splitInstruction[2]);
                        break;
                    }
                    PC++;
                    break;
            }

            return true;
        }
    }
}
