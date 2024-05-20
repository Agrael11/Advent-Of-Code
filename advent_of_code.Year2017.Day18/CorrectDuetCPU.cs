using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day18
{
    internal class CorrectDuetCPU
    {

        public CorrectDuetCPU(int programID)
        {
            SetRegister("p", programID);
        }

        private readonly long[] Registers = new long[25];
        private long PC = 0;
        private readonly Queue<long> Messages = new Queue<long>();

        public Action<long>? SendMessageEvent;

        public void GetMessage(long msg)
        {
            Messages.Enqueue(msg);
        }

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

        public bool DoStep(string[] instructions)
        {
            if (PC < instructions.Length)
            {
                return ExecuteInstruction(instructions[PC]);
            }
            return false;
        }

        public bool ExecuteInstruction(string instruction)
        {
            var splitInstruction = instruction.Split(' ');
            switch (splitInstruction[0])
            {
                case "snd":
                    SendMessageEvent?.Invoke(GetValueOrRegister(splitInstruction[1]));
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
                    if (Messages.Count > 0)
                    {
                        SetRegister(splitInstruction[1], Messages.Dequeue());
                        PC++;
                        return true;
                    }
                    return false;
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
