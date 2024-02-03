using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2015.Day23
{
    internal class CPU(string[] program, uint regA = 0, uint regB = 0, int startPoint = 0)
    {
        private readonly string[] ROM = program;
        public uint RegA { get; private set; } = regA;
        public uint RegB { get; private set; } = regB;
        private int PC = startPoint;

        public void Run()
        {
            while (PC < ROM.Length)
            {
                DoStep();
            }
        }

        public void DoStep()
        {
            var line = ROM[PC];
            var instruction = line.Split(',');
            var opcode = instruction[0].Split(" ")[0];
            var arg1 = instruction[0].Split(" ")[1];
            PC++;
            switch (opcode)
            {
                case "hlf":
                    if (arg1 == "a") RegA /= 2;
                    else if (arg1 == "b") RegB /= 2;
                    break;
                case "tpl":
                    if (arg1 == "a") RegA *= 3;
                    else if (arg1 == "b") RegB *= 3;
                    break;
                case "inc":
                    if (arg1 == "a") RegA++;
                    else if (arg1 == "b") RegB++;
                    break;
                case "jmp":
                    PC += int.Parse(arg1) - 1;
                    break;
                case "jie":
                    if ((arg1 == "a" && (RegA % 2 == 0)) || (arg1 == "b" && (RegB % 2 == 0)))
                    {
                        PC += int.Parse(instruction[1]) - 1;
                    }
                    break;
                case "jio":
                    if ((arg1 == "a" && (RegA == 1)) || (arg1 == "b" && (RegB == 1)))
                    {
                        PC += int.Parse(instruction[1]) - 1;
                    }
                    break;
            }
        }
    }
}
