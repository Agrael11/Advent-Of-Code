using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day12
{
    internal class CPU
    {
        public Dictionary<int, int> Registers;
        private int PC = 0;
        private delegate void OpCode(int arg1, int arg2);
        private readonly List<(OpCode code, int arg1, int arg2)> opcodes;


        public CPU(int a, int b, int c, int d, string[] instructions)
        {
            Registers = new Dictionary<int, int>();
            Registers[0] = a;
            Registers[1] = b;
            Registers[2] = c;
            Registers[3] = d;

            PC = 0;

            opcodes = new List<(OpCode code, int arg1, int arg2)>();

            foreach (var instruction in instructions) 
            {
                var splitInst = instruction.Split(' ');
                switch (splitInst[0])
                {
                    case "cpy":
                        if (int.TryParse(splitInst[1], out var valueCpy))
                        {
                            opcodes.Add((CpyImd, valueCpy, splitInst[2][0] - 'a'));
                        }
                        else
                        {
                            opcodes.Add((CpyReg, splitInst[1][0] - 'a', splitInst[2][0] - 'a'));
                        }

                        break;

                    case "inc":
                        opcodes.Add((Inc, splitInst[1][0] - 'a', -1));

                        break;

                    case "dec":
                        opcodes.Add((Dec, splitInst[1][0] - 'a', -1));

                        break;

                    case "jnz":
                        if (int.TryParse(splitInst[1], out var valueJnz))
                        {
                            opcodes.Add((JnzImd, valueJnz, int.Parse(splitInst[2])));
                        }
                        else
                        {
                            opcodes.Add((JnzReg, splitInst[1][0]-'a', int.Parse(splitInst[2])));
                        }

                        break;
                }
            }
        }

        public void Run()
        {
            PC = 0;
            while (PC < opcodes.Count)
            {
                opcodes[PC].code(opcodes[PC].arg1, opcodes[PC].arg2);
            }
        }

        private void CpyReg(int reg1, int reg2)
        {
            Registers[reg2] = Registers[reg1];
            PC++;
        }

        private void CpyImd(int val, int reg)
        {
            Registers[reg] = val;
            PC++;
        }

        private void Inc(int reg, int nop)
        {
            Registers[reg]++;
            PC++;
        }
        private void Dec(int reg, int nop)
        {
            Registers[reg]--;
            PC++;
        }

        private void JnzReg(int reg, int val)
        {
            if (Registers[reg] != 0) PC += val;
            else PC++;
        }

        private void JnzImd(int val1, int val2)
        {
            if (val1 != 0) PC += val2;
            else PC++;
        }
    }
}
