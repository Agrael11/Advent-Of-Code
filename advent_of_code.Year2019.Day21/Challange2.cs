using Visualizers;

namespace advent_of_code.Year2019.Day21
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            if (AOConsole.Enabled) AOConsole.WriteLine("Starting challange 2\n\n");
            var input = inputData.Replace("\r", "").Replace("\n", "").
               Split(',').Select(long.Parse).ToArray();

            //T = A AND B AND C - all three of the next three tiles must be ground
            //J = NOT T - jump if any of those next three tiles are holes (not all three are ground)
            //J = J AND D - but only if the fourth tile is ground (we need somewhere to land) - at this point J means "we can make first jump"
            //Everything next matters ONLY if J is already true
            //T = NOT D - sicne we know D is true, T is now false
            //T = T OR E OR H - if either E or H are ground, we can make it safely to the next jump - at this point T means "we should survive at least for next 4 blocks"
            //J = T AND J - just - combine previously known "we can make first jump" with "we should survive at least for next 4 blocks"
            var code = "OR A T\nAND B T\nAND C T\nNOT T J\nAND D J\nNOT D T\nOR E T\nOR H T\nAND T J\nRUN\n";

            var computer = new IntMachine.Machine([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);
            computer.RAM = (IntMachine.Memory)input;
            foreach (var c in code)
            {
                computer.PushInput(c);
            }
            var runResult = computer.Run();
            var result = -1L;
            if (runResult == IntMachine.Machine.RunResult.Exit)
            {
                while (computer.TryPopOutput(out var value) && value is not null)
                {
                    if (value.Value > 255)
                    {
                        result = value.Value;
                        if (AOConsole.Enabled) AOConsole.Write(result.ToString());
                    }
                    else if (AOConsole.Enabled)
                    {
                        AOConsole.Write(((char)value.Value).ToString());
                    }
                }
            }
            if (AOConsole.Enabled) AOConsole.WriteLine("\n\n");
            return result;
        }
    }
}