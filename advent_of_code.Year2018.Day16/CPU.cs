using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day16
{
    internal class CPU
    {
        private int[] Registers = [0, 0, 0, 0];
        private readonly Dictionary<string, Action<int,int,int>> Instructions;
        private readonly List<string> UnknownInstructions;
        private readonly Dictionary<int, List<string>> PossibleInstructions;
        private readonly Dictionary<int, Action<int, int, int>> KnownInstructions;

        public CPU(int r0 = 0, int r1 = 0, int r2 = 0, int r3 = 0)
        {
            Registers = [r0, r1, r2, r3];

            Instructions = new Dictionary<string, Action<int, int, int>>();
            UnknownInstructions = new List<string>();
            PossibleInstructions = new Dictionary<int, List<string>>();
            KnownInstructions = new Dictionary<int, Action<int, int, int>>();

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
            
            foreach ((var instruction, _) in Instructions)
            {
                UnknownInstructions.Add(instruction);
            }

            for (var i = 0; i < UnknownInstructions.Count; i++)
            {
                PossibleInstructions[i] = new List<string>(UnknownInstructions);
            }
        }

        public void SetRegister(int index, int value)
        {
            if (index < 0 || index >= Registers.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            Registers[index] = value;
        }

        public void SetRegisters(int r0, int r1, int r2, int r3)
        {
            Registers = [r0, r1, r2, r3];
        }

        public int GetRegister(int index)
        {
            if (index < 0 || index >= Registers.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Registers[index];
        }
        public int[] GetRegisters()
        {
            return [..Registers];
        }

        public bool CompareRegisters(int r0, int r1, int r2, int r3)
        {
            return (r0 == Registers[0]) && (r1 == Registers[1]) && (r2 == Registers[2]) && (r3 == Registers[3]);
        }

        public void ExecuteInstruction(string instructionName, int arg0, int arg1, int arg2)
        {
            Instructions[instructionName].Invoke(arg0, arg1, arg2);
        }

        public void ExecuteOpcode(int opcode, int arg0, int arg1, int arg2)
        {
            KnownInstructions[opcode].Invoke(arg0, arg1, arg2);
        }

        public List<string> TestOpcode(int arg0, int arg1, int arg2, 
            (int start, int expected) r0, (int start, int expected) r1, (int start, int expected) r2, (int start, int expected) r3)
        {
            var possible = new List<string>();

            foreach (var instruction in UnknownInstructions)
            {
                SetRegisters(r0.start, r1.start, r2.start, r3.start);
                ExecuteInstruction(instruction, arg0, arg1, arg2);
                if (CompareRegisters(r0.expected, r1.expected, r2.expected, r3.expected))
                {
                    possible.Add(instruction);
                }
            }

            return possible;
        }

        public void LimitOpcode(int opcode, int arg0, int arg1, int arg2,
            (int start, int expected) r0, (int start, int expected) r1, (int start, int expected) r2, (int start, int expected) r3)
        {
            if (KnownInstructions.ContainsKey(opcode))
                return;

            PossibleInstructions[opcode] = TestOpcode(arg0, arg1, arg2, r0, r1, r2, r3);

            if (PossibleInstructions[opcode].Count == 1)
            {
                AssingInstruction(opcode, PossibleInstructions[opcode].First());
            }
        }

        private void AssingInstruction(int opcode, string instruction)
        {
            KnownInstructions.Add(opcode, Instructions[instruction]);
            PossibleInstructions.Remove(opcode);

            foreach (var possibleInstruction in PossibleInstructions.Values)
            {
                possibleInstruction.Remove(instruction);
            }

            UnknownInstructions.Remove(instruction);
        }

        public bool CheckSingles()
        {
            for (var i = 0; i < PossibleInstructions.Count; i++)
            {
                var key = PossibleInstructions.Keys.ElementAt(i);
                if (PossibleInstructions[key].Count == 1)
                {
                    AssingInstruction(key, PossibleInstructions[key].First());
                    return true;
                }
            }

            for (var i = 0; i < UnknownInstructions.Count; i++)
            {
                var instruction = UnknownInstructions[i];
                var appeared = 0;
                var appearedIn = -1;
                foreach ((var opcode, var possible) in PossibleInstructions)
                {
                    if (possible.Contains(instruction))
                    {
                        appearedIn = opcode;
                        appeared++;
                        if (appearedIn > 1) 
                            break;
                    }
                }
                if (appeared == 1)
                {
                    AssingInstruction(appearedIn, instruction);
                    return true;
                }
            }
            return false;
        }

        public bool AllAssigned()
        {
            return UnknownInstructions.Count == 0;
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
