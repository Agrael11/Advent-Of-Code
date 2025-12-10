using Microsoft.Z3;

namespace advent_of_code.Year2025.Day10
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t => new Machine(t)).ToList();

            var result = 0;

            //Similarly to day 1 - we calculate minimum steps required for each machine
            foreach (var machine in input)
            {
                result += SolveUsingZ3(machine);
            }

            return result;
        }

        /// <summary>
        /// But this time we do this using Z3
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static int SolveUsingZ3(Machine machine)
        {
            //Loads the require native library if needed (z3 only ships with win-x64 and mac-x64)
            Z3Wrapper.Z3NativeLoader.Load();

            var context = new Context();
            var xs = new List<IntExpr>();

            var solver = context.MkOptimize();

            var expression = (ArithExpr)context.MkInt(0);

            //We create the resulting "xs" an the expression to get total
            for (var i = 0; i < machine.ButtonCount; i++)
            {
                var intexp = context.MkIntConst($"x{i}");
                xs.Add(intexp);
                solver.Assert(context.MkGe(intexp, context.MkInt(0)));
                expression = context.MkAdd(expression, intexp);
            }

            //and here we create expressions to calculate how all buttons influence the (every) single light
            for (var i = 0; i < machine.JoltagesCount; i++)
            {
                var list = new List<ArithExpr>();

                for (var j = 0; j < machine.ButtonCount; j++)
                {
                    if (machine.Buttons[j].Contains(i))
                    {
                        list.Add(xs[j]);
                    }
                }

                var expr = context.MkAdd(list.ToArray());
                var result = context.MkEq(expr, context.MkInt(machine.Joltages[i]));
                solver.Add(result);
            }

            //We set it to find minimum
            solver.MkMinimize(expression);

            //And if it's possible to caluclate
            if (solver.Check() != Status.SATISFIABLE)
            {
                throw new Exception($"{solver.Check()}");
            }

            //If so, we get the total minimal "x"
            return ((IntNum)solver.Model.Eval(expression)).Int;
        }
    }
}