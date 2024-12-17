namespace advent_of_code.Year2024.Day17
{
    internal class CPU
    {
        private readonly int[] Registers = [0, 0, 0];

        private readonly List<int> ROM;
        private int PC = 0;
        private bool Running = false;

        private readonly Func<int, bool>[] instructions;
        
        //Event for output
        public EventHandler<CPUOutputEventArgs>? OutputEvent;

        public CPU (int regA, int regB, int regC, List<int> Program)
        {
            ROM = Program;
            Registers[0] = regA;
            Registers[1] = regB;
            Registers[2] = regC;

            instructions = [Adv, Bxl, Bst, Jnz, Bxc, Out, Bdv, Cdv];
        }

        public void Run()
        {
            Running = true;
            while (Running && (PC < (ROM.Count - 1)))
            {
                var opcode = ROM[PC] % 8;
                var operand = ROM[PC+1];
                if (instructions[opcode].Invoke(operand))
                {
                    PC += 2;
                }
            }
        }

        //Converts literal operand to combo operand (who came up with that?)
        private bool Combo(int literal, out int combo)
        {
            combo = literal;
            if (literal > 6) return false;
            if (literal > 3)
            {
                combo = Registers[(literal - 4)];
            }
            return true;
        }

        //Load of instructions
        private bool Adv(int op)
        {
            if (!Combo(op, out var combo)) return true;

            Registers[0] = (int)(Registers[0] / Math.Pow(2, combo));
            return true;
        }

        private bool Bdv(int op)
        {
            if (!Combo(op, out var combo)) return true;

            Registers[1] = (int)(Registers[0] / Math.Pow(2, combo));
            return true;
        }

        private bool Cdv(int op)
        {
            if (!Combo(op, out var combo)) return true;

            Registers[2] = (int)(Registers[0] / Math.Pow(2, combo));
            return true;
        }

        private bool Bxl(int op)
        {
            Registers[1] = Registers[1] ^ op;
            return true;
        }

        private bool Bst(int op)
        {
            if (!Combo(op, out var combo)) return true;

            Registers[1] = combo % 8;
            return true;
        }
        private bool Jnz(int op)
        {
            if (Registers[0] == 0)
            {
                return true;
            }
            PC = op;
            return false;
        }

        private bool Bxc(int op)
        {
            Registers[1] = Registers[1] ^ Registers[2];
            return true;
        }

        private bool Out(int op)
        {
            if (!Combo(op, out var combo)) return true;

            var args = new CPUOutputEventArgs(combo % 8);

            OutputEvent?.Invoke(this, args);
            if (args.Cancel) Running = false;

            return true;
        }
    }
}
