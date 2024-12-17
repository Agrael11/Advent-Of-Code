using System.Text;

namespace advent_of_code.Year2024.Day17
{
    internal static class Disassembler
    {
        public static string Disasemble(List<int> Program)
        {
            Func<int, string>[] instructions = [Adv, Bxl, Bst, Jnz, Bxc, Out, Bdv, Cdv];
            var buidler = new StringBuilder();
            for (var pc = 0; pc < Program.Count; pc+=2)
            {
                var result = instructions[Program[pc]].Invoke(Program[pc+1]);
                buidler.AppendLine($"{pc}: {result} //{Program[pc]},{Program[pc+1]}");
            }
            return buidler.ToString();
        }

        private static string Adv(int op)
        {
            if (!Combo(op, out var combo)) return "Incorrect Instruction";

            return $"A = A / Math.Pow(2,{combo});";
        }

        private static string Bdv(int op)
        {
            if (!Combo(op, out var combo)) return "Incorrect Instruction";

            return $"B = A / Math.Pow(2,{combo});";
        }

        private static string Cdv(int op)
        {
            if (!Combo(op, out var combo)) return "Incorrect Instruction";

            return $"C = A / Math.Pow(2,{combo});";
        }

        private static string Bxl(int op)
        {
            return $"B = B ^ {op}";
        }

        private static string Bst(int op)
        {
            if (!Combo(op, out var combo)) return "Incorrect Instruction";

            return $"B = {combo} % 8";
        }
        private static string Jnz(int op)
        {
            return $"if (A != 0) goto {op};";
        }

        private static string Bxc(int op)
        {
            return $"B = B ^ C";
        }

        private static string Out(int op)
        {
            if (!Combo(op, out var combo)) return "Incorrect Instruction";
            
            return $"Write({combo} % 8);";
        }
        
        private static bool Combo(int literal, out string combo)
        {
            combo = "";

            if (literal > 6) return false;

            combo = literal switch
            {
                4 => "A",
                5 => "B",
                6 => "C",
                _ => literal.ToString(),
            };

            return true;
        }
    }
}
