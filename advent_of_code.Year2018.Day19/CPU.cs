namespace advent_of_code.Year2018.Day19
{
    internal class CPU
    {
        private int[] Registers = [0, 0, 0, 0, 0, 0];
        private readonly Dictionary<string, Action<int, int, int>> Instructions;

        public CPU(int r0 = 0, int r1 = 0, int r2 = 0, int r3 = 0, int r4 = 0, int r5 = 0)
        {
            Registers = [r0, r1, r2, r3, r4, r5];

            Instructions = new Dictionary<string, Action<int, int, int>>();

            Instructions.Add("addr", Inst_addr);
            Instructions.Add("addi", Inst_addi);
            Instructions.Add("mulr", Inst_mulr);
            Instructions.Add("muli", Inst_muli);
            Instructions.Add("banr", Inst_banr);
            Instructions.Add("bani", Inst_bani);
            Instructions.Add("borr", Inst_borr);
            Instructions.Add("bori", Inst_bori);
            Instructions.Add("setr", Inst_setr);
            Instructions.Add("seti", Inst_seti);
            Instructions.Add("gtir", Inst_gtir);
            Instructions.Add("gtri", Inst_gtri);
            Instructions.Add("gtrr", Inst_gtrr);
            Instructions.Add("eqir", Inst_eqir);
            Instructions.Add("eqri", Inst_eqri);
            Instructions.Add("eqrr", Inst_eqrr);
        }

        public (int a, int b, int c, int d, int e) IdentifyRegisters(List<(string intruction, int arg0, int arg1, int arg2)> instructions)
        {
            var a = instructions[7].arg2;
            var b = instructions[2].arg2;
            var c = instructions[7].arg0;
            var d = instructions[4].arg2;
            var e = instructions[4].arg1;
            return (a, b, c, d, e);
        }

        public void Patch(int ip, int a, int b, int c, int d, int e)
        {
            for (; Registers[c] <= Math.Sqrt(Registers[e]); Registers[c]++)
            {
                if (Registers[e] % Registers[c] == 0)
                {
                    Registers[a] += Registers[c];
                    Registers[a] += Registers[e]/Registers[c];
                }
            }
            Registers[b] = Registers[e];
            Registers[c] = Registers[e];
            Registers[d] = 1;
            Registers[ip] = 257;
        }

        public void Run(string[] programTemplate)
        {
            var ipAdd = int.Parse(programTemplate[0].Split(' ')[1]);

            var program = ParseProgram(programTemplate);
            var ip = Registers[ipAdd];
            var (a, b, c, d, e) = IdentifyRegisters(program);

            while (ip >= 0 && ip < program.Count)
            {
                if (ip == 2)
                {
                    Patch(ipAdd, a, b, c, d, e);
                    ip = Registers[ipAdd];
                    continue;
                }
                var line = program[ip];
                ExecuteInstruction(line.intruction, line.arg0, line.arg1, line.arg2);
                Registers[ipAdd]++;
                ip = Registers[ipAdd];
            }
        }

        private List<(string intruction, int arg0, int arg1, int arg2)> ParseProgram(string[] programTemplate)
        {
            var program = new List<(string intruction, int arg0, int arg1, int arg2)>();
            for (var i = 1; i < programTemplate.Length; i++)
            {
                var splitLine = programTemplate[i].Split(' ');
                program.Add((splitLine[0], int.Parse(splitLine[1]), int.Parse(splitLine[2]), int.Parse(splitLine[3])));
            }
            return program;
        }


        public void SetRegister(int index, int value)
        {
            if (index < 0 || index >= Registers.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            Registers[index] = value;
        }

        public int GetRegister(int index)
        {
            if (index < 0 || index >= Registers.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Registers[index];
        }

        public void ExecuteInstruction(string instructionName, int arg0, int arg1, int arg2)
        {
            Instructions[instructionName].Invoke(arg0, arg1, arg2);
        }

        private void Inst_addr(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] + Registers[input2];
        }
        private void Inst_addi(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] + input2;
        }
        private void Inst_mulr(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] * Registers[input2];
        }
        private void Inst_muli(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] * input2;
        }
        private void Inst_banr(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] & Registers[input2];
        }
        private void Inst_bani(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] & input2;
        }
        private void Inst_borr(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] | Registers[input2];
        }
        private void Inst_bori(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1] | input2;
        }
        private void Inst_setr(int input1, int input2, int output)
        {
            Registers[output] = Registers[input1];
        }
        private void Inst_seti(int input1, int input2, int output)
        {
            Registers[output] = input1;
        }
        private void Inst_gtir(int input1, int input2, int output)
        {
            Registers[output] = (input1 > Registers[input2]) ? 1 : 0;
        }
        private void Inst_gtri(int input1, int input2, int output)
        {
            Registers[output] = (Registers[input1] > input2) ? 1 : 0;
        }
        private void Inst_gtrr(int input1, int input2, int output)
        {
            Registers[output] = (Registers[input1] > Registers[input2]) ? 1 : 0;
        }
        private void Inst_eqir(int input1, int input2, int output)
        {
            Registers[output] = (input1 == Registers[input2]) ? 1 : 0;
        }
        private void Inst_eqri(int input1, int input2, int output)
        {
            Registers[output] = (Registers[input1] == input2) ? 1 : 0;
        }
        private void Inst_eqrr(int input1, int input2, int output)
        {
            Registers[output] = (Registers[input1] == Registers[input2]) ? 1 : 0;
        }
    }
}
