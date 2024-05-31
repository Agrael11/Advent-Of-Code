using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day23
{
    internal class CPU
    {
        private readonly Dictionary<string, long> registers = new Dictionary<string, long>();
        private long PC = 0;
        public Action? MulCalled = null;


        public CPU(long a = 0, long b = 0, long c = 0, long d = 0, long e = 0, long f = 0, long g = 0, long h = 0) 
        {
            PC = 0;
            registers["a"] = a;
            registers["b"] = b;
            registers["c"] = c;
            registers["d"] = d;
            registers["e"] = e;
            registers["f"] = f;
            registers["g"] = g;
            registers["h"] = h;
        }

        public void Run(string[] instructions)
        {
            while (PC >= 0 && PC < instructions.Length)
            {
                Step(instructions[PC]);
            }
        }

        public void Step(string instruction)
        {
            var instructionData = instruction.Split(' ');
            var value1 = GetRegisterOrValue(instructionData[1]);
            var value2 = GetRegisterOrValue(instructionData[2]);
            switch (instructionData[0])
            {
                case "set":
                    SetRegister(instructionData[1], value2);
                    PC++;
                    break;
                case "sub":
                    SetRegister(instructionData[1], value1 - value2);
                    PC++;
                    break;
                case "mul":
                    SetRegister(instructionData[1], value1 * value2);
                    MulCalled?.Invoke();
                    PC++;
                    break;
                case "jnz":
                    if (value1 == 0)
                    {
                        PC++;
                    }
                    else
                    {
                        PC += value2;
                    }
                    break;

            }
        }

        private void SetRegister(string register, long value)
        {
            registers[register] = value;
        }

        public long GetRegisterOrValue(string input)
        {
            if (int.TryParse(input, out var value))
            {
                return value;
            }
            return registers[input];
        }
    }
}
