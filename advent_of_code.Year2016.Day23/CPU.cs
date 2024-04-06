using System.ComponentModel;
using System.Security.Cryptography;

namespace advent_of_code.Year2016.Day23
{

    internal class CPU
    {
        /*
        * 0 = Cpy VALUE VALUE (3 length) - INVALID
        * 1 = Cpy VALUE REGISTER (3 length)
        * 2 = Cpy REGISTER VALUE (3 length) - INVALID
        * 3 = Cpy REGISTER REGISTER (3 length)
        * 10 = Inc VALUE (2 length) - INVALID
        * 11 = Inc REGISTER (2 length)
        * 12 = Dec VALUE (2 length) - INVALID
        * 13 = Dec REGISTER (2 length)
        * 20 = Jnz VALUE VALUE (3 length)
        * 21 = Jnz VALUE REGISTER (3 length)
        * 22 = Jnz REGISTER VALUE (3 length)
        * 23 = Jnz REGISTER REGISTER (3 length)
        * 30 = Tgl REGISTER (2 length)
        * 31 = Tgl VALUE (2 length)
        * 99 = Nop (0 length)
        */

        public Dictionary<int, int> Registers;
        private int PC = 0;
        public List<int> ProgramRAM = new List<int>();
        public Dictionary<int, Func<bool>?> InstructionTable = new Dictionary<int, Func<bool>?>();
        public Dictionary<int, (string name, int length)> InstructionTypes = new Dictionary<int, (string name, int length)>();
        public Dictionary<int, int> ToggleTable = new Dictionary<int, int>();

        public CPU(int a, int b, int c, int d)
        {
            Registers = new Dictionary<int, int>();
            Registers[0] = a;
            Registers[1] = b;
            Registers[2] = c;
            Registers[3] = d;

            PC = 0;

            RegisterInstruction(0, "cpy", 3, null);
            RegisterInstruction(1, "cpy", 3, CpyValReg);
            RegisterInstruction(2, "cpy", 3, null);
            RegisterInstruction(3, "cpy", 3, CpyRegReg);
            RegisterInstruction(10, "inc", 2, null);
            RegisterInstruction(11, "inc", 2, Inc);
            RegisterInstruction(12, "dec", 2, null);
            RegisterInstruction(13, "dec", 2, Dec);
            RegisterInstruction(20, "jnz", 3, JnzValVal);
            RegisterInstruction(21, "jnz", 3, JnzValReg);
            RegisterInstruction(22, "jnz", 3, JnzRegVal);
            RegisterInstruction(23, "jnz", 3, JnzRegReg);
            RegisterInstruction(30, "tgl", 2, TglVal);
            RegisterInstruction(31, "tgl", 2, TglReg);
            RegisterInstruction(99, "nop", 1, Nop);

            //2 long:
            ToggleTable[10] = 12;
            ToggleTable[11] = 13;
            ToggleTable[12] = 10;
            ToggleTable[13] = 11;
            ToggleTable[30] = 10;
            ToggleTable[31] = 11;

            //3 long:
            ToggleTable[0] = 20;
            ToggleTable[1] = 21;
            ToggleTable[2] = 22;
            ToggleTable[3] = 23;
            ToggleTable[20] = 0;
            ToggleTable[21] = 1;
            ToggleTable[22] = 2;
            ToggleTable[23] = 3;
        }

        private void RegisterInstruction(int opcode, string name, int length, Func<bool>? function)
        {
            InstructionTypes[opcode] = (name, length);
            InstructionTable[opcode] = function;
        }

        public void SaveBinary(string file)
        {
            using var writer = new BinaryWriter(File.Open(file, FileMode.Create));
            foreach (var data in ProgramRAM)
            {
                writer.Write(data);
            }
            writer.Close();
        }

        public void LoadBinary(string file)
        {
            using var writer = new BinaryReader(File.Open(file, FileMode.Open));
            ProgramRAM.Clear();
            while (writer.PeekChar() != -1)
            {
                ProgramRAM.Add(writer.ReadInt32());
            }
            writer.Close();
        }

        public void Compile(string[] instructions)
        {
            ProgramRAM.Clear();

            foreach (var instruction in instructions)
            {
                var splitInst = instruction.Split(' ');
                switch (splitInst[0])
                {
                    case "cpy":
                        if (int.TryParse(splitInst[1], out var valueCpy))
                        {
                            ProgramRAM.Add(1);
                            ProgramRAM.Add(valueCpy);
                            ProgramRAM.Add(splitInst[2][0] - 'a');
                        }
                        else
                        {
                            ProgramRAM.Add(3);
                            ProgramRAM.Add(splitInst[1][0] - 'a');
                            ProgramRAM.Add(splitInst[2][0] - 'a');
                        }

                        break;

                    case "inc":
                        ProgramRAM.Add(11);
                        ProgramRAM.Add(splitInst[1][0] - 'a');
                        ProgramRAM.Add(0);

                        break;

                    case "dec":
                        ProgramRAM.Add(13);
                        ProgramRAM.Add(splitInst[1][0] - 'a');
                        ProgramRAM.Add(0);

                        break;

                    case "jnz":
                        int? valueJnz1 = null;
                        int? valueJnz2 = null;
                        if (int.TryParse(splitInst[1], out var valueTmp)) valueJnz1 = valueTmp;
                        if (int.TryParse(splitInst[2], out valueTmp)) valueJnz2 = valueTmp;
                        if (valueJnz1 is not null && valueJnz2 is not null)
                        {
                            ProgramRAM.Add(20);
                            ProgramRAM.Add(valueJnz1.Value);
                            ProgramRAM.Add(valueJnz2.Value);
                        }
                        if (valueJnz1 is not null && valueJnz2 is null)
                        {
                            ProgramRAM.Add(21);
                            ProgramRAM.Add(valueJnz1.Value);
                            ProgramRAM.Add(splitInst[2][0] - 'a');
                        }
                        if (valueJnz1 is null && valueJnz2 is not null)
                        {
                            ProgramRAM.Add(22);
                            ProgramRAM.Add(splitInst[1][0] - 'a');
                            ProgramRAM.Add(valueJnz2.Value);
                        }
                        if (valueJnz1 is null && valueJnz2 is null)
                        {
                            ProgramRAM.Add(23);
                            ProgramRAM.Add(splitInst[1][0] - 'a');
                            ProgramRAM.Add(splitInst[2][0] - 'a');
                        }

                        break;

                    case "tgl":
                        if (int.TryParse(splitInst[1], out var valueTgl))
                        {
                            ProgramRAM.Add(30);
                            ProgramRAM.Add(valueTgl);
                            ProgramRAM.Add(0);
                        }
                        else
                        {
                            ProgramRAM.Add(31);
                            ProgramRAM.Add(splitInst[1][0] - 'a');
                            ProgramRAM.Add(0);
                        }

                        break;
                }
            }
        }

        [Hack("Applies All Pattern Hacks")]
        private bool ApplyPatternHacks()
        {
            if (TryApplyMultiplicationHack()) return true;
            if (TryApplyAdditionHack()) return true;
            return false;
        }


        [Hack("Detects multiplication pattern and solves it using math instead")]
        private bool TryApplyMultiplicationHack()
        {
            if (PC + 9 >= ProgramRAM.Count) return false;
            if (InstructionTypes[ProgramRAM[PC + 12]].name != "jnz" || ProgramRAM[PC + 14] != -5) return false;
            if (InstructionTypes[ProgramRAM[PC + 9]].name != "dec") return false;
            if (InstructionTypes[ProgramRAM[PC + 6]].name != "jnz" || ProgramRAM[PC + 8] != -2) return false;

            if (InstructionTypes[ProgramRAM[PC]].name == "inc" &&
                InstructionTypes[ProgramRAM[PC + 3]].name == "dec" &&
                ProgramRAM[PC + 7] == ProgramRAM[PC + 4])
            {

                Registers[ProgramRAM[PC + 1]] += Registers[ProgramRAM[PC + 4]] * Registers[ProgramRAM[PC + 10]];
                Registers[ProgramRAM[PC + 4]] = 0;
                Registers[ProgramRAM[PC + 10]] = 0;

                PC += 15;
                return true;
            }

            if (InstructionTypes[ProgramRAM[PC]].name == "dec" &&
                InstructionTypes[ProgramRAM[PC + 3]].name == "inc" &&
                ProgramRAM[PC + 7] == ProgramRAM[PC + 1])
            {


                Registers[ProgramRAM[PC + 4]] += Registers[ProgramRAM[PC + 1]] * Registers[ProgramRAM[PC + 10]];
                Registers[ProgramRAM[PC + 1]] = 0;
                Registers[ProgramRAM[PC + 10]] = 0;

                PC += 15;
                return true;
            }

            return false;
        }

        [Hack("Detects addition pattern and solves it using math instead")]
        private bool TryApplyAdditionHack()
        {
            if (PC + 9 >= ProgramRAM.Count) return false;
            if (InstructionTypes[ProgramRAM[PC + 6]].name != "jnz" || ProgramRAM[PC + 8] != -2)  return false;

            if (InstructionTypes[ProgramRAM[PC]].name == "inc" &&
                InstructionTypes[ProgramRAM[PC + 3]].name == "dec" &&
                ProgramRAM[PC + 7] == ProgramRAM[PC + 4])
            {

                Registers[ProgramRAM[PC + 1]] += Registers[ProgramRAM[PC + 4]];
                Registers[ProgramRAM[PC + 4]] = 0;

                PC += 9;
                return true;
            }

            if (InstructionTypes[ProgramRAM[PC]].name == "dec" &&
                InstructionTypes[ProgramRAM[PC + 3]].name == "inc" &&
                ProgramRAM[PC + 7] == ProgramRAM[PC + 1])
            {


                Registers[ProgramRAM[PC + 4]] += Registers[ProgramRAM[PC + 1]];
                Registers[ProgramRAM[PC + 1]] = 0;

                PC += 9;
                return true;
            }

            return false;
        }

        public void Run()
        {
            PC = 0;
            while (PC < ProgramRAM.Count)
            {
                if (ApplyPatternHacks()) continue;

                if (!TryGetInstruction(ProgramRAM[PC], out var instruction))
                {
                    PC += 3;
                }
                
                if (instruction.Invoke())
                {
                    PC += 3;
                }
            }
        }

        private bool TryGetInstruction(int address, out Func<bool> instruction)
        {
            instruction = Nop;
            if (!InstructionTable.TryGetValue(address, out var inst)) return false;
            if (inst is null) return false;
            instruction = inst;
            return true;
        }

        private bool Nop()
        {
            return false;
        }

        private bool CpyValReg()
        {
            Registers[ProgramRAM[PC+2]] = ProgramRAM[PC + 1];
            return true;
        }

        private bool CpyRegReg()
        {
            Registers[ProgramRAM[PC + 2]] = Registers[ProgramRAM[PC + 1]];
            return true;
        }

        private bool Inc()
        {
            Registers[ProgramRAM[PC + 1]]++;
            return true;
        }

        private bool Dec()
        {
            Registers[ProgramRAM[PC + 1]]--;
            return true;
        }

        private bool JnzValVal()
        {
            if (ProgramRAM[PC + 1] != 0)
            {
                return Jump(ProgramRAM[PC + 2]);
            }
            return true;
        }

        private bool JnzValReg()
        {
            if (ProgramRAM[PC + 1] != 0)
            {
                return Jump(Registers[ProgramRAM[PC + 2]]);
            }
            return true;
        }

        private bool JnzRegVal()
        {
            if (Registers[ProgramRAM[PC + 1]] != 0)
            {
                return Jump(ProgramRAM[PC + 2]);
            }
            return true;
        }

        private bool JnzRegReg()
        {
            if (Registers[ProgramRAM[PC + 1]] != 0)
            {
                return Jump(Registers[ProgramRAM[PC + 2]]);
            }
            return true;
        }

        private bool Jump(int count)
        {
            if (count != 0)
            {
                PC += count * 3;
                return false;
            }
            return true;
        }

        private bool TglVal()
        {
            Toggle(PC + ProgramRAM[PC + 1] * 3);
            return true;
        }
        private bool TglReg()
        {
            Toggle(PC + Registers[ProgramRAM[PC + 1]] * 3);
            return true;
        }

        private void Toggle(int target)
        {
            if (target >= ProgramRAM.Count) return;

            var instruction = ProgramRAM[target];
            if (ToggleTable.TryGetValue(instruction, out var newInstruction)) ProgramRAM[target] = newInstruction;

        }
    }
}
