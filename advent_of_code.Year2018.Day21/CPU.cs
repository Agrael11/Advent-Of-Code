namespace advent_of_code.Year2018.Day21
{
    internal class CPU
    {
        public Registers Registers;
        private readonly Dictionary<string, Action<int, int, int>> Instructions;

        public CPU(int r0 = 0, int r1 = 0, int r2 = 0, int r3 = 0, int r4 = 0, int r5 = 0)
        {
            Registers = new Registers(r0, r1, r2, r3, r4, r5);

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

        private readonly Dictionary<int, int> RegistryMapping = new Dictionary<int, int>();

        public long Run(string[] programTemplate)
        {
            var ipAdd = int.Parse(programTemplate[0].Split(' ')[1]);

            var program = ParseProgram(programTemplate);
            MapRegisters(program);

            var ip = Registers[ipAdd];
            var executions = 0L;

            while (ip >= 0 && ip < program.Count)
            {
                if (ip == 20)
                {
                    Registers[RegistryMapping[5]] = (Registers[RegistryMapping[3]] / 256);
                    Registers[RegistryMapping[2]] = 1;
                    Registers[ipAdd] = 26;
                    ip = Registers[ipAdd];
                    continue;
                }
                executions++;
                var (intruction, arg0, arg1, arg2) = program[(int)ip];
                ExecuteInstruction(intruction, arg0, arg1, arg2);
                Registers[ipAdd]++;
                ip = Registers[ipAdd];
            }

            return executions;
        }

        private void MapRegisters(List<(string intruction, int arg0, int arg1, int arg2)> program)
        {
            //R0
            RegistryMapping[0] = 0;
            //R1
            RegistryMapping[1] = program[3].arg2;
            //R2
            RegistryMapping[2] = program[18].arg2;
            //R3
            RegistryMapping[3] = program[6].arg2;
            //R4
            RegistryMapping[4] = program[0].arg2;
            //R5
            RegistryMapping[5] = program[8].arg2;
        }

        private static List<(string intruction, int arg0, int arg1, int arg2)> ParseProgram(string[] programTemplate)
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

        public long GetRegister(int index)
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



        public static string TranslateInstruction(string line, int IP)
        {
            var sline = line.Split(' ');
            var instruction = sline[0];
            var arg1 = int.Parse(sline[1]);
            var arg2 = int.Parse(sline[2]);
            var arg3 = int.Parse(sline[3]);
            var output = TranslateRegister(arg3, IP);
            var arg1r = TranslateRegister(arg1, IP);
            var arg2r = TranslateRegister(arg2, IP);

            return instruction switch
            {
                "addr" => $"{output} = {arg1r} + {arg2r}; //{line}",
                "addi" => $"{output} = {arg1r} + {arg2}; //{line}",
                "mulr" => $"{output} = {arg1r} * {arg2r}; //{line}",
                "muli" => $"{output} = {arg1r} * {arg2}; //{line}",
                "banr" => $"{output} = {arg1r} & {arg2r}; //{line}",
                "bani" => $"{output} = {arg1r} & {arg2}; //{line}",
                "borr" => $"{output} = {arg1r} | {arg2r}; //{line}",
                "bori" => $"{output} = {arg1r} | {arg2}; //{line}",
                "setr" => $"{output} = {arg1r}; //{line}",
                "seti" => $"{output} = {arg1}; //{line}",
                "gtrr" => $"{output} = {arg1r} > {arg2r}; //{line}",
                "gtri" => $"{output} = {arg1r} > {arg2}; //{line}",
                "gtir" => $"{output} = {arg1} > {arg2r}; //{line}",
                "eqrr" => $"{output} = {arg1r} == {arg2r}; //{line}",
                "eqri" => $"{output} = {arg1r} == {arg2}; //{line}",
                "eqir" => $"{output} = {arg1} == {arg2r}; //{line}",

                _ => $"Unkown Instruction; //{line}",
            };
        }

        private static string TranslateRegister(int reg, int IP)
        {
            if (reg == IP) return "IP";
            return $"R{reg}";
        }
    }
}